namespace Backend.Entities.Bridge;

public class Pickup
{
    public IHeal? _healing;
    public IAttack? _attack;
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
            unit = new UnitHealDecorator(unit);
        }
        if (_attack != null)
        {
            unit = _attack.AddDamage(unit);
            unit = new UnitDamageDecorator(unit);
        }
        return unit;
    }
}