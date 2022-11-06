using Backend.Entities;

namespace Backend.Utilities.Strategy;

public class TankMoveAlgorithm : MoveAlgorithm
{
    public override bool Move(Tile tile) => 
        MoveTank(tile);

    private bool MoveTank(Tile tile) => 
        !tile.IsObstacle && tile.Unit is null;
}