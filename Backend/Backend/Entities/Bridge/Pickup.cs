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
    public void OnPickup(Unit unit)
    {

    }
}