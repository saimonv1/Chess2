namespace Backend.Entities.Bridge;

public class Pickup
{
    private IAttack _healing;
    private IAttack _attack;
    public Pickup(IAttack healing, IAttack attacking)
    {
        this._healing = healing;
        this._attack = attacking;
    }
    public void OnPickup(Unit unit)
    {

    }
}