#region

#endregion

using Backend.Utilities.Template;

namespace Backend.Utilities.Strategy;

public class ShortRangeShootingAlgorithm : ShootingAlgorithm
{
    public ShortRangeShootingAlgorithm()
    {
        shootingTemplate = new ShortShootingTemplate();
    }
    public override List<Shot> ShootLeft(int posX, int posY, int damage) =>
        shootingTemplate.GetShots(damage, posX - 1, posY);

    public override List<Shot> ShootRight(int posX, int posY, int damage) =>
        shootingTemplate.GetShots(damage, posX + 1, posY);

    public override List<Shot> ShootUp(int posX, int posY, int damage) =>
        shootingTemplate.GetShots(damage, posX, posY - 1);

    public override List<Shot> ShootDown(int posX, int posY, int damage) =>
        shootingTemplate.GetShots(damage, posX, posY + 1);
}