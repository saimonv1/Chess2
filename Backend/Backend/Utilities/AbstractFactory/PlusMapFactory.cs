#region

using Backend.Entities;
using Backend.Enums;

#endregion

namespace Backend.Utilities.AbstractFactory;

public class PlusMapFactory : MapFactory
{
    public override Map GenerateMap(List<Player> players)
    {
        const int size_x = 20;
        const int size_y = 20;

        var corner_size = 6;

        var map = MapPrototype.Map;

        for (var i = 0; i < size_x; i++)
        {
            for (var j = 0; j < size_y; j++)
            {
                if ((i <= corner_size || i >= size_x - corner_size - 1) && j <= corner_size) // top half
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
                else if ((i <= corner_size || i >= size_x - corner_size - 1) &&
                         j >= size_y - corner_size - 1) // bottom half
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