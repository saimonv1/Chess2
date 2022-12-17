using Backend.Flyweight;
namespace Backend.Entities;

public class Tile : ICloneable
{
    public bool IsObstacle { get; private set; } = false;
    public bool IsRevert { get; set; } = false;

    protected Tile()
    {

    }

    public Tile(bool obstacle, bool isRevert = false)
    {
        IsObstacle = obstacle;
        IsRevert = isRevert;
    }

    public virtual object Clone()
    {
        return IsObstacle ? TileFlyweight.obstacleTile : IsRevert ? TileFlyweight.revertTile : TileFlyweight.emptyTile;
    }
}