namespace Backend.Utilities.Template;

public class ShortShootingTemplate : ShootingTemplate
{
    public override bool IsLongRange() => 
        false;

    public override void SetDamage(int initialDamage)
    {
        Damage = initialDamage + 1;
    }

    public override void SetSharpnel()
    {
        throw new NotSupportedException();
    }
}