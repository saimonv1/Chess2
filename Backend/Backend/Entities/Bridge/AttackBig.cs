namespace Backend.Entities.Bridge
{
    public class AttackBig : IAttack
    {
        public Unit AddDamage(Unit unit)
        {
            unit.Damage += 30;
            return unit;
        }
    }
}
