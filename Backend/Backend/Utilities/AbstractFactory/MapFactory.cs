#region

using Backend.Entities;
using Backend.Utilities.Factory;


#endregion

namespace Backend.Utilities.AbstractFactory;

public abstract class MapFactory
{
    protected Creator _creator = new UnitFactory();
    public abstract Map GenerateMap(List<Player> players);
}
