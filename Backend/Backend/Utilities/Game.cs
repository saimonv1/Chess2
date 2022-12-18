#region

using Backend.Entities;
using Backend.Enums;
using Backend.Utilities.AbstractFactory;
using Backend.Utilities.Strategy;
using Backend.Flyweight;
using Backend.Memento;
using Backend.GameHubs;
using Microsoft.AspNetCore.SignalR;

#endregion

namespace Backend.Utilities;

public class Game
{
    private static readonly Game _game = new();
    private static List<Player> _connectedPlayers { get; } = new(4);
    private static Subject _mapSubject { get; } = new();
    private static MapFactory _mapFactory = new PlusMapFactory();

    private static List<IMemento> _mapMementos = new();

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
        BackupMap();
        var playersWithTurns = _connectedPlayers.Where(x => x.Units.Count > 0 && x.Units.Any(x => x.CurrentHealth > 0)).ToList();
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
        Random rnd = new Random();
        var number = rnd.Next(0, 3);
        _mapFactory = number switch
        {
            0 => new EmptyMapFactory(),
            1 => new OMapFactory(),
            2 => new PlusMapFactory(),
            3 => new RandomMapFactory(),
        };
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

    public void MoveItem(int oldX, int oldY, int newX, int newY, string connectionId)
    {
        var newMap = _mapSubject.Map;

        if (newMap.Tiles[newX, newY].IsRevert)
        {
            newMap.Tiles[newX, newY].IsRevert = false;
            var player = _connectedPlayers.First(x => x.ConnectionID == connectionId);
            GameHub.Instance.Clients.Client(player.ConnectionID).SendAsync("SetCanRevert", true);
            //UndoMap();
        }

        var pickup = newMap.Tiles[newX, newY] as TilePickup;
        var unit = newMap.Tiles[oldX, oldY] as TileUnit;

        if (pickup is not null && unit is not null)
        {
            newMap.Tiles[oldX, oldY] = new TileUnit(pickup.Pickup.OnPickup(unit.Unit));
            newMap.Tiles[newX, newY] = TileFlyweight.emptyTile;
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

                if (map.Tiles[i, j] is TileUnit enemy)
                {
                    enemy.Unit.TakeDamage(shot);
                    _mapSubject.Map = map;
                    // if (enemy.Unit.CurrentHealth <= 0)
                    // {
                    //     map.Tiles[i, j] = TileFlyweight.emptyTile;
                    //     _mapSubject.Map = map;
                    // }
                }
            }
        }
    }

    public void RefreshMoves(string connectionId)
    {
        var unit = _connectedPlayers.First(x => x.ConnectionID == connectionId).Units.First();
        unit.RemainingTurns = unit.MovesPerTurn;
        unit.PowerupObject.RemoveTurn();
        unit.PowerupObject.RemoveExpiredPickups();
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
    public void BackupMap()
    {
        _mapMementos.Add(_mapSubject.Save());
    }
    public void UndoMap()
    {
        Console.WriteLine("Restoring map");
        if (_mapMementos.Count < 1)
        {
            return;
        }

        var memento = _mapMementos[_mapMementos.Count - 2];
        _mapMementos.Remove(memento);

        _mapSubject.Restore(memento);
    }
}