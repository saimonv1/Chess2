#region

using System.Linq.Expressions;
using Backend.Entities;
using Backend.Enums;

#endregion

namespace Backend.Utilities;

public class Game
{
    private static readonly Game _game = new();
    private static List<Player> _connectedPlayers { get; set; }
    private static Subject _mapSubject { get; set; }

    public static bool IsGameStarting =>
        _connectedPlayers.Count > 1 && _connectedPlayers.All(p => p.IsReady);

    private static int CurrentPlayerTurn = 0;

    private Game()
    {
        _connectedPlayers = new List<Player>(4);
        _mapSubject = new Subject();
    }

    public static Game GetGameInstance()
    {
        return _game;
    }
    
    public string NextPlayer()
    {
        var connectionId = _connectedPlayers[CurrentPlayerTurn].ConnectionID;
        CurrentPlayerTurn = CurrentPlayerTurn == _connectedPlayers.Count - 1 ? 0 : CurrentPlayerTurn + 1;
        return connectionId;
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
        _mapSubject.Map = MapFactory.GenerateMap(_connectedPlayers);
        return _mapSubject.Map;
    }

    public (int,int,int,int) MoveItem(string connectionId, int move)
    {
        var unit = _connectedPlayers.First(p => p.ConnectionID == connectionId).Units.First();

        var (oldX, oldY) = (unit.PosX, unit.PosY);
        var (newX, newY) = (oldX, oldY);
        (newX, newY) = move switch
        {
            0 => (oldX, oldY - 1),
            1 => (oldX + 1, oldY),
            2 => (oldX, oldY + 1),
            3 => (oldX - 1, oldY),
            _ => (newX, newY)
        };

        if (_mapSubject.Map.Tiles[newX, newY].IsObstacle || _mapSubject.Map.Tiles[newX, newY].Unit is not null)
            return (-1, -1, -1, -1);

        _connectedPlayers.First(p => p.ConnectionID == connectionId).Units[0].PosX = newX;
        _connectedPlayers.First(p => p.ConnectionID == connectionId).Units[0].PosY = newY;
        var newMap = _mapSubject.Map;
        (newMap.Tiles[oldX, oldY], newMap.Tiles[newX, newY]) = (newMap.Tiles[newX, newY], newMap.Tiles[oldX, oldY]);
        _mapSubject.Map = newMap;
        //(Map.Tiles[oldX, oldY], Map.Tiles[newX, newY]) = (Map.Tiles[newX, newY], Map.Tiles[oldX, oldY]);
        return (oldX, oldY, newX, newY);
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