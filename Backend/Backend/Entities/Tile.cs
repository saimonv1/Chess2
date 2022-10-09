namespace Backend.Entities;

public class Tile
{
    public bool IsObstacle { get; set; } = false;
    public Pickup? Pickup { get; set; } = null;
    public Unit? Unit { get; set; } = null;
}