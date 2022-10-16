using Backend.Entities;
using Backend.Enums;
using Backend.Utilities.Factory;

namespace Backend.Utilities.AbstractFactory;

public class EmptyMapFactory : MapFactory
{
    private Creator _creator = new UnitFactory();
    public override Map GenerateMap(List<Player> players)
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