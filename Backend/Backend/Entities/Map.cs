using Backend.Entities.Iterator;

namespace Backend.Entities;

public class Map : ICloneable
{
    public Tile[,] Tiles { get; set; }

    public TileIterator getIterator()
    {
        return new TileIterator(this.Tiles);
    }

    public object Clone()
    {
        Console.WriteLine("Cloning map...");
        var map = new Map();
        map.Tiles = new Tile[this.Tiles.GetLength(0), this.Tiles.GetLength(1)];
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                map.Tiles[i, j] = (Tile)Tiles[i, j].Clone();
            }
        }
        Console.WriteLine("Cloned map");
        Console.WriteLine(map);
        return map;
    }

    public void RemoveRevert()
    {
        var iterator = getIterator();
        var first = iterator.First();
        if (first.IsRevert)
        {
            first.IsRevert = false;
        }
        while (!iterator.IsDone())
        {
            var item = iterator.Current();

            if (item.IsRevert)
            {
                item.IsRevert = false;
            }
            iterator.Next();
        }
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
                else if(tile.IsRevert)
                {
                    mapString += "O";
                }
                else if(tile.GetType() == typeof(TileUnit))
                {
                    mapString += "U";
                }
                else if(tile.GetType() == typeof(TilePickup))
                {
                    mapString += "P";
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