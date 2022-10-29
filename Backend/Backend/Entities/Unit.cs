#region

using Backend.Enums;

#endregion

namespace Backend.Entities;

public abstract class Unit
{
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int MovesPerTurn { get; set; }
    public int RemainingTurns { get; set; }
    public int Damage { get; set; }
    public bool IsAerial { get; set; }
    public Color Color { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
}