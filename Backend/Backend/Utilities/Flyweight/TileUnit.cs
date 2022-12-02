namespace Backend.Entities;

public class TileUnit : Tile
{
    public Unit Unit { get; set; }

    public TileUnit(Unit unit)
    {
        Unit = unit;
    }
}