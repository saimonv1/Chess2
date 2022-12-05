#region

using Backend.Entities.Bridge;
using Backend.Enums;
using Backend.Utilities.State;
using Backend.Utilities.Strategy;

#endregion

namespace Backend.Entities;

public abstract class Unit
{
    private State State { get; set; }
    public virtual int CurrentHealth { get; set; }
    public virtual int MaxHealth { get; set; }
    public virtual int MovesPerTurn { get; set; }
    public virtual int RemainingTurns { get; set; }
    public virtual int Damage { get; set; }
    public virtual bool IsAerial { get; set; }
    public virtual bool IsDestroyed { get; set; }
    public virtual Color Color { get; set; }
    public virtual int PosX { get; set; }
    public virtual int PosY { get; set; }
    public virtual string Label { get; set; }
    
    private ShootingAlgorithm _shootingAlgorithm = new ShortRangeShootingAlgorithm();

    protected Unit()
    {
        State = new FullyHealedState(this);
    }
    
    public abstract string GetLabel();

    public ShootingAlgorithm GetShootingAlgorithm() =>
        _shootingAlgorithm;

    public void SetShootingAlgorithm(ShootingAlgorithm shootingAlgorithm) =>
        _shootingAlgorithm = shootingAlgorithm;

    public Shot Shoot(int move) =>
        move switch
        {
            0 => _shootingAlgorithm.ShootUp(this),
            1 => _shootingAlgorithm.ShootRight(this),
            2 => _shootingAlgorithm.ShootDown(this),
            3 => _shootingAlgorithm.ShootLeft(this),
            _ => new Shot()
        };

    public void Heal(int health) =>
        State.Heal(health);

    public void TakeDamage(Shot shot) =>
        State.TakeDamage(shot);

    public void Destroy() =>
        State.Destroy();

    public void Resurrect() =>
        State.Resurrect();

    public void ChangeState(State state) =>
        State = state;
}