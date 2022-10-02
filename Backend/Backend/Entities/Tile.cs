namespace Backend.Entities;

public class Tile
{
    public bool IsObstacle { get; set; }
    public Pickup? Pickup { get; set; }
}