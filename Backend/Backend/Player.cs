using System;
using System.Collections.Generic;

namespace Backend;

public class Player
{
    public string ConnectionID { get; set; }
    public string Name { get; set; }
    public Color Color { get; set; }
    public List<Unit> Units { get; set; }
    
    public Player(string id, string name, Color color, List<Unit> units)
    {
        ConnectionID = id;
        Name = name;
        Color = color;
        Units = units;
    }
}