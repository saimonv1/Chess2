using Backend.Entities;

namespace Backend.Utilities.Adapter
{
    public class HeliAdapter : Heli
    {
        private Copter _helicopter;

        public HeliAdapter(Copter newHeli)
        {
            _helicopter = newHeli;

            this.CurrentHealth = _helicopter.CurrentHealth;
            this.MovesPerTurn = _helicopter.MovesPerTurn;
            this.RemainingTurns = _helicopter.RemainingTurns;
            this.Damage = _helicopter.Damage;
            this.Color = _helicopter.Color;
            this.PosX = _helicopter.PosX;
            this.PosY = _helicopter.PosY;

            this.IsAerial = true;
            this.MaxHealth = 2;
            this.Label = "Heli";

        }

        public override string GetLabel()
        {
            return this.Label;
        }
    }
}
