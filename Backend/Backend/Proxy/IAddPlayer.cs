using Backend.Entities;
using Backend.Enums;
using Backend.Utilities.Facade;

namespace Backend.Proxy
{
    public interface IAddPlayer
    {
        AddPlayerState AddPlayer(BestFacade facade, Player player);
    }
}
