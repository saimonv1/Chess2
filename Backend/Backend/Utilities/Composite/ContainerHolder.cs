namespace Backend.Utilities.Composite;

public class ContainerHolder : PowerupObject
{
    public override void Add(PowerupObject powerupObject)
    {
        if (powerupObject is DamagePowerup)
        {
            var container = children.FirstOrDefault(x => x is DamagePowerupContainer);
            if (container is null)
            {
                container = new DamagePowerupContainer();
                children.Add(container);
            }

            container.Add(powerupObject);
        }

        if (powerupObject is HealthPowerup)
        {
            var container = children.FirstOrDefault(x => x is HealthContainer);
            if (container is null)
            {
                container = new HealthContainer();
                children.Add(container);
            }

            container.Add(powerupObject);
        }

        if (powerupObject is not DamagePowerupContainer or ContainerHolder or HealthContainer)
        {
            return;
        }

        powerupObject.SetParent(this);
        children.Add(powerupObject);
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
        children.Where(x => x is DamagePowerupContainer).ToList().ForEach(x => damage += x.GetAdditionalDamage());
        children.Where(x => x is ContainerHolder).ToList().ForEach(x => damage += x.GetAdditionalDamage());
        return damage;
    }

    public override void RemoveExpiredPickups()
    {
        children.Where(x => x is DamagePowerupContainer or ContainerHolder).ToList()
            .ForEach(x => x.RemoveExpiredPickups());
    }

    public override void RemoveTurn()
    {
        children.ForEach(x => x.RemoveTurn());
    }
}