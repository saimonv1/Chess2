using Backend.Entities.Bridge;

namespace Backend.Utilities.Composite;

public class HealthContainer : PowerupObject
{
    public HealthContainer() {}
    public override void Add(PowerupObject powerupObject)
    {
        if (powerupObject is not HealthPowerup)
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
        throw new NotSupportedException();
    }

    public override void RemoveExpiredPickups()
    {
        throw new NotSupportedException();
    }

    public override void RemoveTurn()
    {
        throw new NotSupportedException();
    }
}