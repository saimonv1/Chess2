#region

#endregion

namespace Backend.Utilities.Strategy;

public class ShortRangeShootingAlgorithm : ShootingAlgorithm
{
    public override Shot ShootLeft(int posX, int posY, int damage) =>
        new()
        {
            Damage = damage,
            PosX = posX - 1,
            PosY = posY
        };

    public override Shot ShootRight(int posX, int posY, int damage) =>
        new()
        {
            Damage = damage,
            PosX = posX + 1,
            PosY = posY
        };

    public override Shot ShootUp(int posX, int posY, int damage) =>
        new()
        {
            Damage = damage,
            PosX = posX,
            PosY = posY - 1
        };

    public override Shot ShootDown(int posX, int posY, int damage) =>
        new()
        {
            Damage = damage,
            PosX = posX,
            PosY = posY + 1
        };
}