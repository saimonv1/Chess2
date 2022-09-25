using System;
using System.Collections.Generic;

namespace Backend;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Color Color { get; set; }
    public List<Unit> Units { get; set; }
    
    public Player(Guid id, string name, Color color, List<Unit> units)
    {
        Id = id;
        Name = name;
        Color = color;
        Units = units;
    }
}