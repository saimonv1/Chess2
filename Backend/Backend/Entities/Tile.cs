using Backend.Flyweight;
namespace Backend.Entities;

public class Tile : ICloneable
{
    public bool IsObstacle { get; set; } = false;

    protected Tile()
    {

    }

    public Tile(bool obstacle)
    {
        IsObstacle = obstacle;
    }

    public object Clone()
    {
        return IsObstacle ? TileFlyweight.obstacleTile : TileFlyweight.emptyTile;
    }
}