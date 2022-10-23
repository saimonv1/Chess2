#region

using Backend.Entities;
using Backend.Enums;

#endregion

namespace Backend.Utilities.AbstractFactory;

public class EmptyMapFactory : MapFactory
{
    public override Map GenerateMap(List<Player> players)
    {
        // for easier map size change for different player amounts
        const int size_x = 20;
        const int size_y = 20;

        var map = MapPrototype.Map;

        for (var i = 0; i < size_x; i++)
        {
            for (var j = 0; j < size_y; j++)
            {
                if (i == 0 || i == size_x - 1 || j == 0 || j == size_y - 1)
                {
                    map.Tiles[i, j].IsObstacle = true;
                }
            }
        }

        foreach (var player in players)
        {
            var teamColor = player.Color;
            player.Units = _creator.GetUnits(teamColor, MapType.Empty);
            var unit = player.Units.First();

            map.Tiles[unit.PosX, unit.PosY].Unit = unit;
        }

        return map;
    }
}