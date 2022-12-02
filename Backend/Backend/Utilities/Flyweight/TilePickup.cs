using Backend.Entities.Bridge;

namespace Backend.Entities;

public class TilePickup : Tile
{
    public Pickup Pickup { get; set; }

    public TilePickup(Pickup pickup)
    {
        Pickup = pickup;
    }
}