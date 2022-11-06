#region

using Backend.Entities;
using Backend.Enums;
using Backend.Utilities.AbstractFactory;
using Backend.Utilities.Strategy;

#endregion

namespace Backend.Utilities;

public class Game
{
    private static readonly Game _game = new();
    private static List<Player> _connectedPlayers { get; } = new(4);
    private static Subject _mapSubject { get; } = new();
    private static MapFactory _mapFactory = new PlusMapFactory();

    public static bool IsGameStarting =>
        _connectedPlayers.Count > 1 && _connectedPlayers.All(p => p.IsReady);

    private static int CurrentPlayerTurn;
    private static string CurrentPlayer = "";

    private Game()
    {
    }

    public static Game GetGameInstance()
    {
        return _game;
    }

    public string NextPlayer()
    {
        var playersWithTurns = _connectedPlayers.Where(x => x.Units.Count > 0).ToList();
        var playerTurn = playersWithTurns.Find(x => x.ConnectionID == CurrentPlayer);
        if (playerTurn is null)
        {
            CurrentPlayerTurn = 0;
        }
        else
        {
            var index = playersWithTurns.FindIndex(x => x == playerTurn);
            CurrentPlayerTurn = index == playersWithTurns.Count - 1 ? 0 : index + 1;
        }
        CurrentPlayer = playersWithTurns[CurrentPlayerTurn].ConnectionID;
        return CurrentPlayer;
    }

    public AddPlayerState AddPlayer(Player player)
    {
        if (_connectedPlayers.Any(p => p.Name == player.Name)) return AddPlayerState.PlayerWithNameExists;

        if (_connectedPlayers.Count == 4) return AddPlayerState.ServerIsFull;

        if (IsGameStarting) return AddPlayerState.GameInProgress;

        _connectedPlayers.Add(player);
        _mapSubject.Subscribe(player);
        return AddPlayerState.Completed;
    }

    public void RemovePlayer(Player player)
    {
        _connectedPlayers.Remove(player);
        _mapSubject.Unsubscribe(player);
    }

    public Player? GetPlayerByConnectionId(string connectionId)
    {
        return _connectedPlayers.FirstOrDefault(p => p.ConnectionID == connectionId);
    }

    public List<Player> GetPlayers()
    {
        return _connectedPlayers.ToList();
    }

    public bool ChangeReadyStatus(string connectionId)
    {
        var player = _connectedPlayers.FirstOrDefault(p => p.ConnectionID == connectionId);
        if (player is not null) player.IsReady = !player.IsReady;

        return player?.IsReady ?? false;
    }

    public Map GenerateMap()
    {
        _mapSubject.Map = _mapFactory.GenerateMap(_connectedPlayers);
        return _mapSubject.Map;
    }

    public void ChangeMap(MapType type)
    {
        _mapFactory = type switch
        {
            MapType.Empty => new EmptyMapFactory(),
            MapType.Plus => new PlusMapFactory(),
            MapType.O => new OMapFactory(),
            MapType.Random => new RandomMapFactory(),
            _ => new EmptyMapFactory()
        };

        _mapSubject.Map = _mapFactory.GenerateMap(_connectedPlayers);
    }

    public void MoveItem(int oldX, int oldY, int newX, int newY)
    {
        var newMap = _mapSubject.Map;
        if (newMap.Tiles[newX, newY].Pickup is not null)
        {
            newMap.Tiles[oldX, oldY].Unit = newMap.Tiles[newX, newY].Pickup.OnPickup(newMap.Tiles[oldX, oldY].Unit);
            newMap.Tiles[newX, newY].Pickup = null;
        }

        (newMap.Tiles[oldX, oldY], newMap.Tiles[newX, newY]) = (newMap.Tiles[newX, newY], newMap.Tiles[oldX, oldY]);
        _mapSubject.Map = newMap;
    }

    public void ChangeShootingType(string connectionId, ShootingAlgorithm shootingAlgorithm)
    {
        var unit = _connectedPlayers.First(x => x.ConnectionID == connectionId).Units.First();
        unit.SetShootingAlgorithm(shootingAlgorithm);
    }

    public void Shoot(string connectionId, int move)
    {
        var map = _mapSubject.Map;
        var unit = _connectedPlayers.First(x => x.ConnectionID == connectionId).Units.First();
        var shot = unit.Shoot(move);
        int maxX, maxY, minX, minY;
        if (shot.PosX > unit.PosX)
        {
            maxX = shot.PosX;
            minX = unit.PosX;
        }
        else
        {
            maxX = unit.PosX;
            minX = shot.PosX;
        }
        if (shot.PosY > unit.PosY)
        {
            maxY = shot.PosY;
            minY = unit.PosY;
        }
        else
        {
            maxY = unit.PosY;
            minY = shot.PosY;
        }
        for (var i = minX; i <= maxX; i++)
        {
            for (var j = minY; j <= maxY; j++)
            {
                if (i == unit.PosX && j == unit.PosY)
                {
                    continue;
                }

                var enemy = map.Tiles[i, j].Unit;
                if (enemy is not null)
                {
                    enemy.CurrentHealth -= shot.Damage;
                    if (enemy.CurrentHealth <= 0)
                    {
                        map.Tiles[i, j].Unit = null;
                        _connectedPlayers.First(x => x.Units.Contains(enemy)).Units.Remove(enemy);
                        _mapSubject.Map = map;
                    }
                }
            }
        }
    }

    public void RefreshMoves()
    {
        foreach (var player in _connectedPlayers)
        {
            foreach (var unit in player.Units)
            {
                unit.RemainingTurns = unit.MovesPerTurn;
            }
        }
    }

    public Map GetMap()
    {
        return _mapSubject.Map;
    }

    public Color GetFirstAvailableFreeColor()
    {
        foreach (var color in Enum.GetValues<Color>())
        {
            if (_connectedPlayers.All(p => p.Color != color))
            {
                return color;
            }
        }

        return Color.Yellow;
    }
}