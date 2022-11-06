namespace Backend.Entities.Bridge
{
    public class AttackSmall : IAttack
    {

        public Unit AddDamage(Unit unit)
        {
            unit.Damage += 15;
            return unit;
        }
    }
}
