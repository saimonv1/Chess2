namespace Backend.Entities;

public class Map : ICloneable
{
    public Tile[,] Tiles { get; set; }

    public object Clone()
    {
        var map = new Map();
        map.Tiles = new Tile[this.Tiles.GetLength(0), this.Tiles.GetLength(1)];
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                map.Tiles[i, j] = (Tile)Tiles[i, j].Clone();
            }
        }
        return map;
    }

    public override string ToString()
    {
        string mapString = "";
        for(int i = 0; i< Tiles.GetLength(0); i++)
        {
            for(int j = 0; j < Tiles.GetLength(1); j++)
            {
                var tile = Tiles[i, j];
                if (tile.IsObstacle)
                {
                    mapString += "X";
                }
                else if(tile.Unit is not null)
                {
                    mapString += "O";
                }
                else
                {
                    mapString += " ";
                }
            }
            mapString += "\n";
        }
        return mapString;
    }
}