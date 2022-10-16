#region

using Backend.Entities;
using Backend.Utilities.Factory;

#endregion

namespace Backend.Utilities.AbstractFactory;

public abstract class MapFactory
{
    protected Creator _creator = new UnitFactory();
    public abstract Map GenerateMap(List<Player> players);
}


// No idea if we should have a random map generation, might be a hassle to make unit generation hard
// public class RandomObstacleMapFactory : IMapFactory
// {
//     public Map GenerateMap(List<Player> players)
//     {
//         var size_x = 20;
//         var size_y = 20;
//
//         var no_obstacle_border_size = 2;
//         var randomness_factor = 25;
//
//         var map = new Map
//         {
//             Tiles = new Tile[size_x, size_y]
//         };
//
//         for (var i = 0; i < size_x; i++)
//         {
//             for (var j = 0; j < size_y; j++)
//             {
//                 map.Tiles[i, j] = new Tile();
//                 var random = new Random();
//
//                 if (i == 0 || i == (size_x - 1) || j == 0 || j == (size_y - 1))
//                 {
//                     map.Tiles[i, j].IsObstacle = true;
//                 }
//                 else if (i > no_obstacle_border_size && i < (size_x - 1 - no_obstacle_border_size) && j > no_obstacle_border_size && j < (size_y - 1 - no_obstacle_border_size) && random.Next(0, randomness_factor) == 0)
//                 {
//                     map.Tiles[i, j].IsObstacle = true;
//                 }
//             }
//         }
//
//         foreach (var unit in players.Select(player => player.Units.First()))
//         {
//             map.Tiles[unit.PosX, unit.PosY].Unit = unit;
//         }
//
//         return map;
//     }
// }