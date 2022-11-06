#region

using Backend.Entities;

#endregion

namespace Backend.Utilities.Strategy;

public abstract class ShootingAlgorithm
{
    public abstract Shot ShootLeft(Unit unit);
    public abstract Shot ShootRight(Unit unit);
    public abstract Shot ShootUp(Unit unit);
    public abstract Shot ShootDown(Unit unit);
}