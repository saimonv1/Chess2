#region

using Backend.Entities;
using Backend.Utilities.State;

#endregion

namespace Backend.Utilities.ChainOfResponsibility;

public class UnitBaseDamageCalculator : DamageCalculator
{
    public override int CalculateDamage(Unit unit)
    {
        DamageSum = unit.State switch
        {
            FullyHealedState => unit.Damage,
            DamagedState => unit.Damage / 2,
            _ => 0
        };
        Console.WriteLine($"Base Damage Calculator added {DamageSum} damage");
        return nextCalculator == null ? DamageSum : DamageSum + nextCalculator.CalculateDamage(unit);
    }
}