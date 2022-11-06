using Backend.Entities;

namespace Backend.Utilities.Strategy;

public class HeliMoveAlgorithm : MoveAlgorithm
{
    public override bool Move(Tile tile) => 
        MoveHeli(tile);

    private bool MoveHeli(Tile tile) =>
        tile.Unit is null;
}