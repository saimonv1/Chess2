using Backend.Entities;

namespace Backend.Utilities.Strategy;

public abstract class MoveAlgorithm
{
    public abstract bool Move(Tile tile);
}