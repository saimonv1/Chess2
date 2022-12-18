#region

#endregion

using Backend.Utilities.Template;

namespace Backend.Utilities.Strategy;

public abstract class ShootingAlgorithm
{
    protected ShootingTemplate shootingTemplate;
    public abstract List<Shot> ShootLeft(int posX, int posY, int damage);
    public abstract List<Shot> ShootRight(int posX, int posY, int damage);
    public abstract List<Shot> ShootUp(int posX, int posY, int damage);
    public abstract List<Shot> ShootDown(int posX, int posY, int damage);
}