using Backend.Entities.Bridge;

namespace Backend.Utilities.Composite;

public class DamagePowerupContainer : PowerupObject
{
    public DamagePowerupContainer() {}
    public override void Add(PowerupObject powerupObject)
    {
        if (powerupObject is not DamagePowerup)
        {
            return;
        }
        children.Add(powerupObject);
        powerupObject.SetParent(this);
    }

    public override void Remove(PowerupObject powerupObject)
    {
        children.Remove(powerupObject);
    }

    public override List<PowerupObject> GetChildren() => 
        children;

    public override void SetParent(PowerupObject powerupObject)
    {
        parent = powerupObject;
    }

    public override PowerupObject GetParent() => 
        parent;
    
    public override int GetAdditionalDamage()
    {
        var damage = 0;
        children.ForEach(x => damage += x.AttackPickup.AddDamage());
        return damage;
    }

    public override void RemoveExpiredPickups() => 
        children.RemoveAll(x => x.RemainingTurns <= 0);

    public override void RemoveTurn()
    {
        children.ForEach(x => x.RemainingTurns--);
    }
}