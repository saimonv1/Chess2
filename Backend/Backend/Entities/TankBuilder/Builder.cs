using Backend.Enums;

namespace Backend.Entities.TankBuilder;

public abstract class Builder
{
    protected Unit Unit;

    public Builder(Unit unit) =>
        Unit = unit;

    public abstract Builder AddMainBody();
    public abstract Builder AddWeaponry();

    public Builder AddCoordinates(int coordX, int coordY)
    {
        Unit.PosX = coordX;
        Unit.PosY = coordY;
        return this;
    }

    public Unit Build() =>
        Unit;
}