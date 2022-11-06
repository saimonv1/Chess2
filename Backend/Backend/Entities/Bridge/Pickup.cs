namespace Backend.Entities.Bridge;

public class Pickup
{
    private IHeal _healing;
    private IAttack _attack;
    public Pickup(IHeal healing, IAttack attacking)
    {
        this._healing = healing;
        this._attack = attacking;
    }
    public Unit OnPickup(Unit unit)
    {
        if (_healing != null)
        {
            unit = _healing.AddHealth(unit);
        }
        if (_attack != null)
        {
            unit = _attack.AddDamage(unit);
        }
        return unit;
    }
}