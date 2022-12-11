#region

using Backend.Entities;
using Backend.Utilities.Strategy;

#endregion

namespace Backend.Utilities.ChainOfResponsibility;

public class ShootingAlgorithmDamageCalculator : DamageCalculator
{
    public override int CalculateDamage(Unit unit)
    {
        DamageSum = unit.GetShootingAlgorithm() switch
        {
            ShortRangeShootingAlgorithm => 1,
            LongRangeShootingAlgorithm => 2,
            _ => 0
        };
        Console.WriteLine($"Shooting Algorithm Damage Calculator added {DamageSum} damage");
        return nextCalculator == null ? DamageSum : DamageSum + nextCalculator.CalculateDamage(unit);
    }
}