using Backend.Entities;
using Backend.Utilities.Strategy;

namespace Backend.Utilities.State;

public abstract class State
{
    protected Unit Unit;

    public State(Unit unit) =>
        Unit = unit;

    public abstract void TakeDamage(Shot shot);
    public abstract void Heal(int i);
    public abstract void Destroy();
    public abstract void Resurrect();
}