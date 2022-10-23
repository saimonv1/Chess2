#region

using Backend.Entities;
using Backend.Enums;

#endregion

namespace Backend.Utilities.AbstractFactory;

public class EmptyMapFactory : MapFactory
{
    public override Map GenerateMap(List<Player> players)
    {
        var map = MapPrototype.Map;

        foreach (var player in players)
        {
            var teamColor = player.Color;
            player.Units = _creator.GetUnits(teamColor, MapType.Empty);
            var unit = player.Units.First();

            map.Tiles[unit.PosX, unit.PosY].Unit = unit;
        }

        return map;
    }

    internal Map GenerateMap()
    {
        throw new NotImplementedException();
    }
}