using Backend.Enums;

namespace Backend.Entities;

public class Tank : Unit
{
    public Tank() {}
    public Tank(Color teamColor, int posX, int posY)
    {
        CurrentHealth = 2;
        Damage = 1;
        IsAerial = false;
        MaxHealth = 2;
        MovesPerTurn = 1;
        Color = teamColor;
        PosX = posX;
        PosY = posY;
    }
}