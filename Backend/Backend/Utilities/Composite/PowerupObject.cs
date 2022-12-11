using Backend.Entities.Bridge;

namespace Backend.Utilities.Composite;

public abstract class PowerupObject
{
    protected PowerupObject parent = null;
    protected List<PowerupObject> children;
    public IHeal? HealPickup;
    public IAttack? AttackPickup;
    public int? RemainingTurns;

    public PowerupObject()
    {
        children = new List<PowerupObject>();
    }

    public PowerupObject(IAttack? attack, int? turns) : this()
    {
        AttackPickup = attack;
        RemainingTurns = turns;
    }

    public PowerupObject(IHeal? heal, int? turns) : this()
    {
        HealPickup = heal;
        RemainingTurns = turns;
    }

    public PowerupObject(IHeal? heal, IAttack? attack, int? turns) : this(heal, turns) => 
        AttackPickup = attack;

    public abstract void Add(PowerupObject powerupObject);
    public abstract void Remove(PowerupObject powerupObject);
    public abstract List<PowerupObject> GetChildren();
    public abstract void SetParent(PowerupObject powerupObject);
    public abstract PowerupObject GetParent();
    public abstract int GetAdditionalDamage();
    public abstract void RemoveExpiredPickups();
    public abstract void RemoveTurn();
}