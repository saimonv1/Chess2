#region

using Backend.Entities;

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
                    map.Tiles[i, j] = new Tile();

                    if (i == 0 || i == size_x - 1 || j == 0 || j == size_y - 1)
                    {
                        map.Tiles[i, j].IsObstacle = true;
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
