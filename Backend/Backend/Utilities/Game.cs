#region

using System.Linq.Expressions;
using Backend.Entities;
using Backend.Enums;

#endregion

namespace Backend.Utilities;

public class Game
{
    private static readonly Game _game = new();
    private static List<Player> ConnectedPlayers { get; set; }
    private static Map Map { get; set; }

    public static bool IsGameStarting =>
        ConnectedPlayers.Count > 1 && ConnectedPlayers.All(p => p.IsReady);

    private static int CurrentPlayerTurn = 0;

    private Game()
    {
        ConnectedPlayers = new List<Player>(4);
    }

    public static Game GetGameInstance()
    {
        return _game;
    }
    
    public string NextPlayer()
    {
        var connectionId = ConnectedPlayers[CurrentPlayerTurn].ConnectionID;
        CurrentPlayerTurn = CurrentPlayerTurn == ConnectedPlayers.Count - 1 ? 0 : CurrentPlayerTurn + 1;
        return connectionId;
    }

    public AddPlayerState AddPlayer(Player player)
    {
        if (ConnectedPlayers.Any(p => p.Name == player.Name)) return AddPlayerState.PlayerWithNameExists;

        if (ConnectedPlayers.Count == 4) return AddPlayerState.ServerIsFull;

        if (IsGameStarting) return AddPlayerState.GameInProgress;

        ConnectedPlayers.Add(player);
        return AddPlayerState.Completed;
    }

    public void RemovePlayer(Player player)
    {
        ConnectedPlayers.Remove(player);
    }

    public Player? GetPlayerByConnectionId(string connectionId)
    {
        return ConnectedPlayers.FirstOrDefault(p => p.ConnectionID == connectionId);
    }

    public List<Player> GetPlayers()
    {
        return ConnectedPlayers.ToList();
    }

    public bool ChangeReadyStatus(string connectionId)
    {
        var player = ConnectedPlayers.FirstOrDefault(p => p.ConnectionID == connectionId);
        if (player is not null) player.IsReady = !player.IsReady;

        return player?.IsReady ?? false;
    }

    public Map GenerateMap()
    {
        Map = MapFactory.GenerateMap(ConnectedPlayers);
        return Map;
    }

    public (int,int,int,int) MoveItem(string connectionId, int move)
    {
        var unit = ConnectedPlayers.First(p => p.ConnectionID == connectionId).Units.First();

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

        if (Map.Tiles[newX, newY].IsObstacle || Map.Tiles[newX, newY].Unit is not null)
            return (-1, -1, -1, -1);

        ConnectedPlayers.First(p => p.ConnectionID == connectionId).Units[0].PosX = newX;
        ConnectedPlayers.First(p => p.ConnectionID == connectionId).Units[0].PosY = newY;
        (Map.Tiles[oldX, oldY], Map.Tiles[newX, newY]) = (Map.Tiles[newX, newY], Map.Tiles[oldX, oldY]);
        return (oldX, oldY, newX, newY);
    }

    public Color GetFirstAvailableFreeColor()
    {
        foreach (var color in Enum.GetValues<Color>())
        {
            if (ConnectedPlayers.All(p => p.Color != color))
            {
                return color;
            }
        }

        return Color.Yellow;
    }
}