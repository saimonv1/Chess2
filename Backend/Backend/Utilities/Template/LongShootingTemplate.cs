using Backend.Utilities.Strategy;

namespace Backend.Utilities.Template;

public class LongShootingTemplate : ShootingTemplate
{
    public override bool IsLongRange() => 
        true;

    public override void SetDamage(int initialDamage)
    {
        Damage = initialDamage + 2;
    }

    public override void SetSharpnel()
    {
        shots.Add(new Shot{Damage = Damage/2, PosX = PosX + 1, PosY = PosY});
        shots.Add(new Shot{Damage = Damage/2, PosX = PosX - 1, PosY = PosY});
        shots.Add(new Shot{Damage = Damage/2, PosX = PosX, PosY = PosY - 1});
        shots.Add(new Shot{Damage = Damage/2, PosX = PosX, PosY = PosY + 1});
    }
}