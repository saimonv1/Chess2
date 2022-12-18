#region

using Backend.Entities;
using Backend.Entities.Bridge;
using Backend.Enums;
using Backend.Flyweight;

#endregion

namespace Backend.Utilities.AbstractFactory;

public class RandomMapFactory : MapFactory
{
    public override Map GenerateMap(List<Player> players)
    {
        const int size_x = 20;
        const int size_y = 20;

        var no_obstacle_border_size = 2;
        var randomness_factor = 25;
        var random = new Random();

        var map = MapPrototype.Map;

        for (var i = 0; i < size_x; i++)
        {
            for (var j = 0; j < size_y; j++)
            {
                if (i > no_obstacle_border_size && i < (size_x - 1 - no_obstacle_border_size) && j > no_obstacle_border_size && j < (size_y - 1 - no_obstacle_border_size) && random.Next(0, randomness_factor) == 0)
                {
                    map.Tiles[i, j] = TileFlyweight.obstacleTile;
                }
            }
        }

        foreach (var player in players)
        {
            var teamColor = player.Color;
            player.Units = _creator.GetUnits(teamColor, MapType.Plus);
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