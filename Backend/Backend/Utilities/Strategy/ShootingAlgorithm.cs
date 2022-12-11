#region

#endregion

namespace Backend.Utilities.Strategy;

public abstract class ShootingAlgorithm
{
    public abstract Shot ShootLeft(int posX, int posY, int damage);
    public abstract Shot ShootRight(int posX, int posY, int damage);
    public abstract Shot ShootUp(int posX, int posY, int damage);
    public abstract Shot ShootDown(int posX, int posY, int damage);
}