#region

#endregion

namespace Backend.Utilities.Strategy;

public class LongRangeShootingAlgorithm : ShootingAlgorithm
{
    public override Shot ShootLeft(int posX, int posY, int damage) =>
        new()
        {
            Damage = damage,
            PosX = posX - 2,
            PosY = posY
        };

    public override Shot ShootRight(int posX, int posY, int damage) =>
        new()
        {
            Damage = damage,
            PosX = posX + 2,
            PosY = posY
        };

    public override Shot ShootUp(int posX, int posY, int damage) =>
        new()
        {
            Damage = damage,
            PosX = posX,
            PosY = posY - 2
        };

    public override Shot ShootDown(int posX, int posY, int damage) =>
        new()
        {
            Damage = damage,
            PosX = posX,
            PosY = posY + 2
        };
}