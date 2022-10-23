namespace Backend.Entities;

public class Map : ICloneable
{
    public Tile[,] Tiles { get; set; }

    public object Clone()
    {
        return (Map)this.MemberwiseClone();
    }
}