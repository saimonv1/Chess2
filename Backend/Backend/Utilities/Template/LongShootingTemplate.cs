#region

using Backend.Utilities.Strategy;

#endregion

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
        var shotLeft = PosX == 0 ? PosX : PosX - 1;
        var shotRight = PosX == 19 ? PosX : PosX + 1;
        var shotUp = PosY == 0 ? PosY : PosY - 1;
        var shotDown = PosY == 19 ? PosY : PosY + 1;
        shots.Add(new Shot { Damage = Damage / 2, PosX = shotLeft, PosY = PosY });
        shots.Add(new Shot { Damage = Damage / 2, PosX = shotRight, PosY = PosY });
        shots.Add(new Shot { Damage = Damage / 2, PosX = PosX, PosY = shotUp });
        shots.Add(new Shot { Damage = Damage / 2, PosX = PosX, PosY = shotDown });
    }
}