using Backend.Entities;
using Backend.Enums;
using Backend.Utilities.Facade;

namespace Backend.Proxy
{
    public class AddPlayerProxy : IAddPlayer
    {
        private string[] _badNames;
        private AddPlayerService _addPlayerService = new AddPlayerService();

        public AddPlayerState AddPlayer(BestFacade facade, Player player)
        {
            if(_badNames == null)
            {
                _badNames = new string[]{ "Gintautas", "Simonas" };
            }
            if(_badNames.Contains(player.Name))
            {
                return AddPlayerState.PlayerWithNameExists;
            }
            return _addPlayerService.AddPlayer(facade, player);
        }
    }
}
