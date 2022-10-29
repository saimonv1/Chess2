using Backend.Enums;

namespace Backend.Entities
{
    public class UnitDecorator : Unit
    {
        private Unit _unit;
        public UnitDecorator(Unit unit)
        {
            _unit = unit;
        }
        public override int CurrentHealth { get => _unit.CurrentHealth; set => _unit.CurrentHealth = value; }
        public override int MaxHealth { get => _unit.MaxHealth; set => _unit.MaxHealth = value; }
        public override int MovesPerTurn { get => _unit.MovesPerTurn; set => _unit.MovesPerTurn = value; }
        public override int RemainingTurns { get => _unit.RemainingTurns; set => _unit.RemainingTurns = value; }
        public override int Damage { get => _unit.Damage; set => _unit.Damage = value; }
        public override bool IsAerial { get => _unit.IsAerial; set => _unit.IsAerial = value; }
        public override Color Color { get => _unit.Color; set => _unit.Color = value; }
        public override int PosX { get => _unit.PosX; set => _unit.PosX = value; }
        public override int PosY { get => _unit.PosY; set => _unit.PosY = value; }
        public override string Label { get => _unit.Label; set => _unit.Label = value; }
        public override string GetLabel()
        {
            return _unit.Label;
        }
    }
}
