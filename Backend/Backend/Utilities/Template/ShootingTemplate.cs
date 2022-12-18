using Backend.Utilities.Strategy;

namespace Backend.Utilities.Template;

public abstract class ShootingTemplate
{
    protected int Damage = 0;
    protected int PosX = -1;
    protected int PosY = -1;
    protected List<Shot> shots;
    
    public List<Shot> GetShots(int initialDamage, int x, int y)
    {
        shots = new List<Shot>();
        SetDamage(initialDamage);
        SetInitialShotCoords(x, y);
        if (IsLongRange())
        {
            SetSharpnel();
        }
        return shots;
    }
    
    public abstract bool IsLongRange();
    public abstract void SetDamage(int initialDamage);
    private void SetInitialShotCoords(int x, int y)
    {
        x = x switch
        {
            < 0 => 0,
            > 19 => 19,
            _ => x
        };

        y = y switch
        {
            < 0 => 0,
            > 19 => 19,
            _ => y
        };
        PosX = x;
        PosY = y;
        shots.Add(new Shot { Damage = Damage, PosX = x, PosY = y });
    }
    public abstract void SetSharpnel();
}