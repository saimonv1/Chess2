#region

using Backend.Entities;

#endregion

namespace Backend.Utilities.ChainOfResponsibility;

public abstract class DamageCalculator
{
    protected DamageCalculator nextCalculator;
    protected int DamageSum;

    public void SetNextCalculator(DamageCalculator damageCalculator) =>
        nextCalculator = damageCalculator;

    public abstract int CalculateDamage(Unit unit);
}