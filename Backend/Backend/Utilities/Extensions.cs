using Backend.Entities;

namespace Backend.Utilities;

public static class Extensions
{
    public static Tile[,] Invert(this Tile[,] tiles)
    {
        var inverted = new Tile[tiles.GetLength(0), tiles.GetLength(1)];
        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                inverted[j, i] = tiles[i, j];
            }
        }

        return inverted;
    }
    public static int PickupsCount(this Map map)
    {
        int count = 0;
        var iterator = map.getIterator();
        var first = iterator.First();
        if(first as TilePickup is not null)
        {
            count++;
        }
        while (!iterator.IsDone())
        {
            var item = iterator.Current();

            if(item as TilePickup is not null)
            {
                count++;
            }
            iterator.Next();
        }
        return count;
    }

    public static void FixUnitPositions(this Map map)
    {
        for (int i = 0; i < map.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < map.Tiles.GetLength(1); j++)
            {
                if (map.Tiles[i, j] is TileUnit)
                {
                    (map.Tiles[i, j] as TileUnit).Unit.PosX = i;
                    (map.Tiles[i, j] as TileUnit).Unit.PosY = j;
                }
            }
        }
    }
}