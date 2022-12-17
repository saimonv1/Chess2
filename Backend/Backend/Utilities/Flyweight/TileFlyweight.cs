using Backend.Entities;

namespace Backend.Flyweight;

public static class TileFlyweight
{
    public static Tile emptyTile { get; private set; } = new Tile(false);

    public static Tile obstacleTile { get; private set; } = new Tile(true);
    public static Tile revertTile { get; private set; } = new Tile(false, true);
}