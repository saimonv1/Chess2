using Backend.Entities;
using Backend.Utilities.Strategy;

namespace Backend.Utilities.State;

public class DestroyedState : State
{
    public DestroyedState(Unit unit) : base(unit)
    {
    }

    public override void TakeDamage(Shot shot)
    {
    }

    public override void Heal(int i)
    {
    }

    public override void Destroy()
    {
    }

    public override void Resurrect()
    {
        Unit.CurrentHealth = 1;
        Unit.IsDestroyed = false;
        Unit.ChangeState(new DamagedState(Unit));
    }
}