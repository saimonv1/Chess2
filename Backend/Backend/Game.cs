#region

using Backend.Enums;

#endregion

namespace Backend;

public class Game
{
    private static readonly Game _game = new();
    private static List<Player> ConnectedPlayers { get; set; }
    private static Map Map { get; set; }

    private Game()
    {
        ConnectedPlayers = new List<Player>(4);
    }

    public static Game GetGameInstance()
    {
        return _game;
    }

    public AddPlayerState AddPlayer(Player player)
    {
        if (ConnectedPlayers.Any(p => p.Name == player.Name)) return AddPlayerState.PlayerWithNameExists;

        if (ConnectedPlayers.Count == 4) return AddPlayerState.ServerIsFull;

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