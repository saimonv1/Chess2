#region

using Backend.Entities;
using Backend.Flyweight;

#endregion

namespace Backend.Utilities.AbstractFactory;

public static class MapPrototype
{
    private static Map? _map = null;

    public static Map Map
    {
        get
        {
            if (_map is not null)
            {
                return (Map)_map.Clone();
            }

            // for easier map size change for different player amounts
            const int size_x = 20;
            const int size_y = 20;

            var map = new Map
            {
                Tiles = new Tile[size_x, size_y]
            };

            for (var i = 0; i < size_x; i++)
            {
                for (var j = 0; j < size_y; j++)
                {
                    if (i == 0 || i == size_x - 1 || j == 0 || j == size_y - 1)
                    {
                        map.Tiles[i, j] = TileFlyweight.obstacleTile;
                    }
                    else
                    {
                        map.Tiles[i, j] = TileFlyweight.emptyTile;
                    }
                }
            }

            _map = map;

            return (Map)_map.Clone();
        }
        set
        {
            return;
        }
    }
}
