namespace Backend.Entities.Bridge
{
    public class HealSmall : IHeal
    {
        public Unit AddHealth(Unit unit)
        {
            int additionalHealth = 15;
            unit.MaxHealth = unit.MaxHealth < (unit.CurrentHealth + additionalHealth) ? unit.MaxHealth : (unit.CurrentHealth + additionalHealth);
            return unit;
        }
    }
}
