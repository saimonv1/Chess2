#region

#endregion

using System.Runtime.CompilerServices;
using Backend.Utilities.Template;

namespace Backend.Utilities.Strategy;

public class LongRangeShootingAlgorithm : ShootingAlgorithm
{
    public LongRangeShootingAlgorithm()
    {
        shootingTemplate = new LongShootingTemplate();
    }

    public override List<Shot> ShootLeft(int posX, int posY, int damage) =>
        shootingTemplate.GetShots(damage, posX - 2, posY);

    public override List<Shot> ShootRight(int posX, int posY, int damage) =>
        shootingTemplate.GetShots(damage, posX + 2, posY);

    public override List<Shot> ShootUp(int posX, int posY, int damage) =>
        shootingTemplate.GetShots(damage, posX, posY - 2);

    public override List<Shot> ShootDown(int posX, int posY, int damage) =>
        shootingTemplate.GetShots(damage, posX, posY + 2);
}