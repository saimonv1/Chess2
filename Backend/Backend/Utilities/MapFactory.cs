#region

using Backend.Entities;

#endregion

namespace Backend.Utilities;

public static class MapFactory
{
    public static Map GenerateMap(List<Player> players)
    {
        var map = new Map
        {
            Tiles = new Tile[20,20]
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