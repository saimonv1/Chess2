using System.Collections.Generic;

namespace Backend;

public class Game
{
    public static List<Player> ConnectedPlayers { get; set; } = new List<Player>();
    public static Map Map { get; set; }
}