using Backend.Entities.Bridge;
using Backend.Flyweight;

namespace Backend.Entities;

public class TilePickup : Tile
{
    public Pickup Pickup { get; set; }

    public TilePickup(Pickup pickup)
    {
        Pickup = pickup;
    }
    public override object Clone()
    {
        return new TilePickup(this.Pickup);
    }
}