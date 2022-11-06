namespace Backend.Entities.Bridge
{
    public class HealBig : IHeal
    {
        public Unit AddHealth(Unit unit)
        {
            int additionalHealth = 30;
            unit.MaxHealth = unit.MaxHealth < (unit.CurrentHealth + additionalHealth) ? unit.MaxHealth : (unit.CurrentHealth + additionalHealth);
            return unit;
        }
    }
}
