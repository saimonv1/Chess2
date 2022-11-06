#region

using Backend.Entities;

#endregion

namespace Backend.Utilities.Adapter;

public class HeliAdapter : Heli
{
    private readonly Copter _helicopter;

    public HeliAdapter(Copter newHeli)
    {
        _helicopter = newHeli;

        CurrentHealth = _helicopter.CurrentHealth;
        MovesPerTurn = _helicopter.MovesPerTurn;
        RemainingTurns = _helicopter.RemainingTurns;
        Damage = _helicopter.Damage;
        Color = _helicopter.Color;
        PosX = _helicopter.PosX;
        PosY = _helicopter.PosY;

        IsAerial = true;
        MaxHealth = 2;
        Label = "Heli";
    }

    public override string GetLabel()
    {
        return Label;
    }
}