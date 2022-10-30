namespace Backend.Entities.TankBuilder;

public class TankBuilder : Builder
{
    public TankBuilder(Unit unit) : base(unit) { }

    public override Builder AddMainBody()
    {
        Unit.CurrentHealth = 2;
        Unit.MaxHealth = 2;
        Unit.MovesPerTurn = 3;
        Unit.IsAerial = false;
        Unit.RemainingTurns = Unit.MovesPerTurn;
        Unit.Label = "Tank";
        return this;
    }

    public override Builder AddWeaponry()
    {
        Unit.Damage = 1;
        return this;
    }
}