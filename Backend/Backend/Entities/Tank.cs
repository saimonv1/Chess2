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
        MovesPerTurn = 3;
        RemainingTurns = MovesPerTurn;
        Color = teamColor;
        PosX = posX;
        PosY = posY;
        Label = "Tank";
    }

    public override string GetLabel()
    {
        return Label;
    }

    public void TakeDamage()
    {
        CurrentHealth--;
    }
}