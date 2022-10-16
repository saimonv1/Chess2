using Backend.Entities;
using Backend.Enums;

namespace Backend.Utilities.Factory;

public class UnitFactory : Creator
{
    public override List<Unit> GetUnits(Color teamColor, MapType mapType) =>
        mapType switch
        {
            MapType.Empty => GenerateForEmptyMap(teamColor),
            MapType.Plus => GenerateForPlusMap(teamColor),
            _ => new List<Unit>()
        };

    private List<Unit> GenerateForEmptyMap(Color teamColor) =>
        teamColor switch
        {
            Color.Red => new List<Unit> { new Tank(teamColor, 10, 1) },
            Color.Green => new List<Unit> { new Tank(teamColor, 1, 10) },
            Color.Blue => new List<Unit> { new Tank(teamColor, 10, 18) },
            Color.Yellow => new List<Unit> { new Tank(teamColor, 18, 10) },
            _ => new List<Unit>()
        };

    private List<Unit> GenerateForPlusMap(Color teamColor) =>
        teamColor switch
        {
            Color.Red => new List<Unit> { new Tank(teamColor, 1, 10) },
            Color.Green => new List<Unit> { new Tank(teamColor, 10, 1) },
            Color.Blue => new List<Unit> { new Tank(teamColor, 18, 10) },
            Color.Yellow => new List<Unit> { new Tank(teamColor, 10, 18) },
            _ => new List<Unit>()
        };
}