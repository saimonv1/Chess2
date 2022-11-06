#region

using Backend.Entities;

#endregion

namespace Backend.Utilities.Strategy;

public class LongRangeShootingAlgorithm : ShootingAlgorithm
{
    public override Shot ShootLeft(Unit unit) =>
        new()
        {
            Damage = 1 + unit.Damage,
            PosX = unit.PosX - 2,
            PosY = unit.PosY
        };

    public override Shot ShootRight(Unit unit) =>
        new()
        {
            Damage = 1 + unit.Damage,
            PosX = unit.PosX + 2,
            PosY = unit.PosY
        };

    public override Shot ShootUp(Unit unit) =>
        new()
        {
            Damage = 1 + unit.Damage,
            PosX = unit.PosX,
            PosY = unit.PosY - 2
        };

    public override Shot ShootDown(Unit unit) =>
        new()
        {
            Damage = 1 + unit.Damage,
            PosX = unit.PosX,
            PosY = unit.PosY + 2
        };
}