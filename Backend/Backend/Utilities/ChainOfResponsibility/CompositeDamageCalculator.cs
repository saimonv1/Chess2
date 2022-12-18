using Backend.Entities;

namespace Backend.Utilities.ChainOfResponsibility;

public class CompositeDamageCalculator : DamageCalculator
{
    public override int CalculateDamage(Unit unit)
    {
        DamageSum = unit.PowerupObject.GetAdditionalDamage();
        Console.WriteLine($"Composite Damage Calculator added {DamageSum} damage");
        return nextCalculator == null ? DamageSum : DamageSum + nextCalculator.CalculateDamage(unit);
    }
}