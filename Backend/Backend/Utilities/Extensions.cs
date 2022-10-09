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
}