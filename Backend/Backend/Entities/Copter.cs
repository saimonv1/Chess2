using Backend.Enums;

namespace Backend.Entities
{
    public class Copter
    {
        public virtual int CurrentHealth { get; set; }
        public virtual int MovesPerTurn { get; set; }
        public virtual int RemainingTurns { get; set; }
        public virtual int Damage { get; set; }
        public virtual Color Color { get; set; }
        public virtual int PosX { get; set; }
        public virtual int PosY { get; set; }

        public Copter() { }

        public Copter(Color teamColor, int posX, int posY)
        {
            CurrentHealth = 2;
            Damage = 1;
            MovesPerTurn = 3;
            RemainingTurns = MovesPerTurn;
            Color = teamColor;
            PosX = posX;
            PosY = posY;
        }
    }
}
