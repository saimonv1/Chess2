using Backend.Entities;
using Backend.Enums;
using Backend.Utilities.Facade;

namespace Backend.Proxy
{
    public class AddPlayerService : IAddPlayer
    {
        public AddPlayerState AddPlayer(BestFacade facade, Player player)
        {
            return facade.AddPlayer(player);
        }
    }
}
