using Backend.Enums;

namespace Backend.Entities
{
    public class Heli : Unit
    {
        public Heli() { }
        public Heli(Color teamColor, int posX, int posY)
        {
            CurrentHealth = 2;
            Damage = 2;
            IsAerial = false;
            MaxHealth = 2;
            MovesPerTurn = 2;
            RemainingTurns = MovesPerTurn;
            Color = teamColor;
            PosX = posX;
            PosY = posY;
            Label = "Heli";
        }

        public override string GetLabel()
        {
            return Label;
        }
    }
}
