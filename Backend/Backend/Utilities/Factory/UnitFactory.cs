using Backend.Entities;
using Backend.Entities.TankBuilder;
using Backend.Enums;

namespace Backend.Utilities.Factory;

public class UnitFactory : Creator
{
    public override List<Unit> GetUnits(Color teamColor, MapType mapType) =>
        mapType switch
        {
            MapType.Empty => GenerateForEmptyMap(teamColor),
            MapType.Plus => GenerateForPlusMap(teamColor),
            MapType.O => GenerateForOMap(teamColor),
            MapType.Random => GenerateForRandomMap(teamColor),
            _ => new List<Unit>()
        };

    private List<Unit> GenerateForEmptyMap(Color teamColor)
    {
        var newUnit = new Tank
        {
            Color = teamColor
        };
        var builder = new TankBuilder(newUnit);
        List<Unit> unitList = teamColor switch
        {
            Color.Red => new List<Unit> { new UnitRedDecorator(Director.ConstructTank(builder, 10, 1)) },
            Color.Green => new List<Unit> { new UnitGreenDecorator(Director.ConstructTank(builder, 1, 10)) },
            Color.Blue => new List<Unit> { new UnitBlueDecorator(Director.ConstructTank(builder, 10, 18)) },
            Color.Yellow => new List<Unit> { new UnitYellowDecorator(Director.ConstructTank(builder, 18, 10)) },
            _ => new List<Unit>()
        };
        return unitList;
    }
        

    private List<Unit> GenerateForPlusMap(Color teamColor)
    {
        var newUnit = new Tank
        {
            Color = teamColor
        };
        var builder = new TankBuilder(newUnit);
        List<Unit> unitList = teamColor switch
        {
            Color.Red => new List<Unit> { new UnitRedDecorator(Director.ConstructTank(builder, 1, 10)) },
            Color.Green => new List<Unit> { new UnitGreenDecorator(Director.ConstructTank(builder, 10, 1)) },
            Color.Blue => new List<Unit> { new UnitBlueDecorator(Director.ConstructTank(builder, 18, 10)) },
            Color.Yellow => new List<Unit> { new UnitYellowDecorator(Director.ConstructTank(builder, 10, 18)) },
            _ => new List<Unit>()
        };
        //foreach(var unit in unitList)
        //{
        //    Console.WriteLine(unit.GetLabel());
        //}
        return unitList;
    }
        

    private List<Unit> GenerateForOMap(Color teamColor)
    {
        var newUnit = new Tank
        {
            Color = teamColor
        };
        var builder = new TankBuilder(newUnit);
        List<Unit> unitList = teamColor switch
        {
            Color.Red => new List<Unit> { new UnitRedDecorator(Director.ConstructTank(builder, 1, 10)) },
            Color.Green => new List<Unit> { new UnitGreenDecorator(Director.ConstructTank(builder, 10, 1)) },
            Color.Blue => new List<Unit> { new UnitBlueDecorator(Director.ConstructTank(builder, 18, 10)) },
            Color.Yellow => new List<Unit> { new UnitYellowDecorator(Director.ConstructTank(builder, 10, 18)) },
            _ => new List<Unit>()
        };
        return unitList;
    }
        

    private List<Unit> GenerateForRandomMap(Color teamColor)
    {
        var newUnit = new Tank
        {
            Color = teamColor
        };
        var builder = new TankBuilder(newUnit);
        List<Unit> unitList = teamColor switch
        {
            Color.Red => new List<Unit> { new UnitRedDecorator(Director.ConstructTank(builder, 1, 10)) },
            Color.Green => new List<Unit> { new UnitGreenDecorator(Director.ConstructTank(builder, 10, 1)) },
            Color.Blue => new List<Unit> { new UnitBlueDecorator(Director.ConstructTank(builder, 18, 10)) },
            Color.Yellow => new List<Unit> { new UnitYellowDecorator(Director.ConstructTank(builder, 10, 18)) },
            _ => new List<Unit>()
        };
        return unitList;
    }
}