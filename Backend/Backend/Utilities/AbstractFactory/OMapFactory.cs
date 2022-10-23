#region

using Backend.Entities;
using Backend.Enums;

#endregion

namespace Backend.Utilities.AbstractFactory;

public class OMapFactory : MapFactory
{
    public override Map GenerateMap(List<Player> players)
    {
        var size_x = 20;
        var size_y = 20;

        var no_obstacle_border_size = 4;

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
                else if (i > no_obstacle_border_size && i < (size_x - 1 - no_obstacle_border_size) && j > no_obstacle_border_size && j < (size_y - 1 - no_obstacle_border_size))
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
            }
        }

        foreach (var player in players)
        {
            var teamColor = player.Color;
            player.Units = _creator.GetUnits(teamColor, MapType.Plus);
            var unit = player.Units.First();

            map.Tiles[unit.PosX, unit.PosY].Unit = unit;
        }

        return map;
    }
}