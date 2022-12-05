using Backend.Entities;
using Backend.Utilities.Strategy;

namespace Backend.Utilities.State;

public class DamagedState : State
{
    public DamagedState(Unit unit) : base(unit)
    {
    }

    public override void TakeDamage(Shot shot)
    {
        Unit.CurrentHealth -= shot.Damage;
        if (Unit.CurrentHealth > 0) return;
        Destroy();
    }

    public override void Heal(int heal)
    {
        Unit.CurrentHealth += heal;
        if (Unit.CurrentHealth <= Unit.MaxHealth) return;
        Unit.CurrentHealth = Unit.MaxHealth;
        Unit.ChangeState(new FullyHealedState(Unit));
    }

    public override void Destroy()
    {
        Unit.IsDestroyed = true;
        Unit.CurrentHealth = 0;
        Unit.ChangeState(new DestroyedState(Unit));
    }

    public override void Resurrect()
    {
    }
}