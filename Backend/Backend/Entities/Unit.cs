#region

using Backend.Enums;
using Backend.Utilities.ChainOfResponsibility;
using Backend.Utilities.Composite;
using Backend.Utilities.State;
using Backend.Utilities.Strategy;

#endregion

namespace Backend.Entities;

public abstract class Unit
{
    public State State { get; private set; }
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
    public virtual DamageCalculator DamageCalculator { get; set; }
    public virtual PowerupObject PowerupObject { get; set; }

    private ShootingAlgorithm _shootingAlgorithm = new ShortRangeShootingAlgorithm();

    protected Unit()
    {
        State = new FullyHealedState(this);
        PowerupObject = new ContainerHolder();
    }

    public abstract string GetLabel();

    public ShootingAlgorithm GetShootingAlgorithm() =>
        _shootingAlgorithm;

    public void SetShootingAlgorithm(ShootingAlgorithm shootingAlgorithm) =>
        _shootingAlgorithm = shootingAlgorithm;

    public int ShotDamage()
    {
        var damage = DamageCalculator.CalculateDamage(this);
        Console.WriteLine($"Damage calculators returned {damage} damage");
        return damage;
    }

    public Shot Shoot(int move) =>
        move switch
        {
            0 => _shootingAlgorithm.ShootUp(PosX, PosY, ShotDamage()),
            1 => _shootingAlgorithm.ShootRight(PosX, PosY, ShotDamage()),
            2 => _shootingAlgorithm.ShootDown(PosX, PosY, ShotDamage()),
            3 => _shootingAlgorithm.ShootLeft(PosX, PosY, ShotDamage()),
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