#region

using Backend.Entities;
using Backend.Entities.Bridge;
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

            map.Tiles[unit.PosX, unit.PosY] = new TileUnit(unit);
        }

        ///PICKUPS
        map.Tiles[1, 1] = new TilePickup(new Pickup(new Entities.Bridge.HealSmall(), null));
        map.Tiles[18, 18] = new TilePickup(new Pickup(new Entities.Bridge.HealBig(), null));
        map.Tiles[1, 18] = new TilePickup(new Pickup(null, new Entities.Bridge.AttackSmall()));
        map.Tiles[18, 1] = new TilePickup(new Pickup(null, new Entities.Bridge.AttackBig()));
        map.Tiles[9, 9] = new Tile(false, true);
        ///

        return map;
    }
}