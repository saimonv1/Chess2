using Backend.Entities;
using Backend.Utilities.Strategy;

namespace Backend.Utilities.State;

public class FullyHealedState : State
{
    public FullyHealedState(Unit unit) : base(unit)
    {
    }

    public override void TakeDamage(Shot shot)
    {
        Unit.CurrentHealth -= shot.Damage;
        if (Unit.CurrentHealth <= 0)
        {
            Destroy();
            return;
        }
        Unit.ChangeState(new DamagedState(Unit));
    }

    public override void Heal(int i)
    {
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