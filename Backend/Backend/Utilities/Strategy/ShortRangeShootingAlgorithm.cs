#region

using Backend.Entities;

#endregion

namespace Backend.Utilities.Strategy;

public class ShortRangeShootingAlgorithm : ShootingAlgorithm
{
    public override Shot ShootLeft(Unit unit) =>
        new()
        {
            Damage = 2 + unit.Damage,
            PosX = unit.PosX - 1,
            PosY = unit.PosY
        };

    public override Shot ShootRight(Unit unit) =>
        new()
        {
            Damage = 2 + unit.Damage,
            PosX = unit.PosX + 1,
            PosY = unit.PosY
        };

    public override Shot ShootUp(Unit unit) =>
        new()
        {
            Damage = 2 + unit.Damage,
            PosX = unit.PosX,
            PosY = unit.PosY - 1
        };

    public override Shot ShootDown(Unit unit) =>
        new()
        {
            Damage = 2 + unit.Damage,
            PosX = unit.PosX,
            PosY = unit.PosY + 1
        };
}