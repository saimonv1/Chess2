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
        var map = new Map
        {
            Tiles = new Tile[20, 20]
        };

        for (var i = 0; i < 20; i++)
        {
            for (var j = 0; j < 20; j++)
            {
                map.Tiles[i, j] = new Tile();

                if (i is 0 or 19 || j is 0 or 19)
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
        var map = new Map
        {
            Tiles = new Tile[20, 20]
        };

        for (var i = 0; i < 20; i++)
        {
            for (var j = 0; j < 20; j++)
            {
                map.Tiles[i, j] = new Tile();

                if (i is 0 or 19 || j is 0 or 19) // border
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
                if ((i <= 6 || i >= 13) && j <= 6) // top half
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
                else if ((i <= 6 || i >= 13) && j >= 13) // bottom half
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
        var map = new Map
        {
            Tiles = new Tile[20, 20]
        };

        for (var i = 0; i < 20; i++)
        {
            for (var j = 0; j < 20; j++)
            {
                map.Tiles[i, j] = new Tile();

                var random = new Random();

                if (i is 0 or 19 || j is 0 or 19)
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
                else if (i > 2 && i < 18 && j > 2 && j < 18 && random.Next(0, 25) == 0)
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
