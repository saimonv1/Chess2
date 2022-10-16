using Backend.Entities;
using Backend.Enums;

namespace Backend.Utilities.Factory;

public abstract class Creator
{
    public abstract List<Unit> GetUnits(Color teamColor, MapType mapType);
}