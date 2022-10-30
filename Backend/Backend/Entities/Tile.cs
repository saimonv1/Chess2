namespace Backend.Entities;
using Backend.Entities.Bridge;

public class Tile : ICloneable
{
    public bool IsObstacle { get; set; } = false;
    public Pickup? Pickup { get; set; } = null;
    public Unit? Unit { get; set; } = null;

    public object Clone()
    {
        var tile = new Tile();
        tile.IsObstacle = this.IsObstacle;
        tile.Pickup = this.Pickup;
        tile.Unit = this.Unit;
        return tile;
    }
}