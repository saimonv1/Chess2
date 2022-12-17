namespace Backend.Entities;

public class TileUnit : Tile
{
    public Unit Unit { get; set; }

    public TileUnit(Unit unit)
    {
        Unit = unit;
    }
    public override object Clone()
    {
        return new TileUnit(this.Unit);
    }
}