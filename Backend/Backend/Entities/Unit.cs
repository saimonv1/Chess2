﻿#region

using Backend.Enums;

#endregion

namespace Backend.Entities;

public abstract class Unit
{
    public virtual int CurrentHealth { get; set; }
    public virtual int MaxHealth { get; set; }
    public virtual int MovesPerTurn { get; set; }
    public virtual int Damage { get; set; }
    public virtual bool IsAerial { get; set; }
    public virtual Color Color { get; set; }
    public virtual int PosX { get; set; }
    public virtual int PosY { get; set; }
    public virtual string Label { get; set; }
    public abstract string GetLabel();
}