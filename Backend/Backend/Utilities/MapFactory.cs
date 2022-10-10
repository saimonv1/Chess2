#region

using Backend.Entities;

#endregion

namespace Backend.Utilities;

public interface IMapFactory
{
    public abstract Map GenerateMap(List<Player> players);
}

public class EmptyMapFactory : IMapFactory
{
    public Map GenerateMap(List<Player> players)
    {
        // for easier map size change for different player amounts
        var size_x = 20;
        var size_y = 20;

        var map = new Map
        {
            Tiles = new Tile[size_x, size_y]
        };

        for (var i = 0; i < size_x; i++)
        {
            for (var j = 0; j < size_y; j++)
            {
                map.Tiles[i, j] = new Tile();

                if (i == 0 || i == (size_x - 1) || j == 0 || j == (size_y - 1))
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
            }
        }

        foreach (var unit in players.Select(player => player.Units.First()))
        {
            map.Tiles[unit.PosX, unit.PosY].Unit = unit;
        }

        return map;
    }
}

public class PlusMapFactory : IMapFactory
{
    public Map GenerateMap(List<Player> players)
    {
        var size_x = 20;
        var size_y = 20;

        var corner_size = 6;

        var map = new Map
        {
            Tiles = new Tile[size_x, size_y]
        };

        for (var i = 0; i < size_x; i++)
        {
            for (var j = 0; j < size_y; j++)
            {
                map.Tiles[i, j] = new Tile();

                if (i == 0 || i == (size_x - 1) || j == 0 || j == (size_y - 1))
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
                else if ((i <= corner_size || i >= (size_x - corner_size - 1)) && j <= corner_size) // top half
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
                else if ((i <= corner_size || i >= (size_x - corner_size - 1)) && j >= (size_y - corner_size - 1)) // bottom half
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
            }
        }

        foreach (var unit in players.Select(player => player.Units.First()))
        {
            map.Tiles[unit.PosX, unit.PosY].Unit = unit;
        }

        return map;
    }
}

public class RandomObstacleMapFactory : IMapFactory
{
    public Map GenerateMap(List<Player> players)
    {
        var size_x = 20;
        var size_y = 20;

        var no_obstacle_border_size = 2;
        var randomness_factor = 25;

        var map = new Map
        {
            Tiles = new Tile[size_x, size_y]
        };

        for (var i = 0; i < size_x; i++)
        {
            for (var j = 0; j < size_y; j++)
            {
                map.Tiles[i, j] = new Tile();
                var random = new Random();

                if (i == 0 || i == (size_x - 1) || j == 0 || j == (size_y - 1))
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
                else if (i > no_obstacle_border_size && i < (size_x - 1 - no_obstacle_border_size) && j > no_obstacle_border_size && j < (size_y - 1 - no_obstacle_border_size) && random.Next(0, randomness_factor) == 0)
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
            }
        }

        foreach (var unit in players.Select(player => player.Units.First()))
        {
            map.Tiles[unit.PosX, unit.PosY].Unit = unit;
        }

        return map;
    }
}
