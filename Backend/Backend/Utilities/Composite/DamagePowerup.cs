using Backend.Entities.Bridge;

namespace Backend.Utilities.Composite;

public class DamagePowerup : PowerupObject
{
    public DamagePowerup(IAttack? attack, int? turns) : base(attack, turns) {}
    public override void Add(PowerupObject powerupObject)
    {
        throw new NotSupportedException();
    }

    public override void Remove(PowerupObject powerupObject)
    {
        throw new NotSupportedException();
    }

    public override List<PowerupObject> GetChildren()
    {
        throw new NotSupportedException();
    }

    public override void SetParent(PowerupObject powerupObject)
    {
        parent = powerupObject;
    }

    public override PowerupObject GetParent() =>
        parent;

    public override int GetAdditionalDamage() =>
        throw new NotSupportedException();

    public override void RemoveExpiredPickups()
    {
        throw new NotSupportedException();
    }

    public override void RemoveTurn()
    {
        throw new NotSupportedException();
    }
}