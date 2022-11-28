using Backend.Entities;
using Backend.Enums;
using Backend.Utilities.Strategy;

namespace Backend.Utilities.Facade;

public class BestFacade
{
    private readonly Game _game = Game.GetGameInstance();
    
    public Player? GetPlayer(string connectionId) =>
        _game.GetPlayerByConnectionId(connectionId);

    public List<Player> GetPlayers() =>
        _game.GetPlayers();

    public AddPlayerState AddPlayer(Player player) =>
        _game.AddPlayer(player);
    public void RemovePlayer(Player player) =>
        _game.RemovePlayer(player);
    
    public string NextPlayer() =>
        _game.NextPlayer();

    public void Shoot(string connectionId, int move) =>
        _game.Shoot(connectionId, move);

    public void ShortShootingAlgorithm(string connectionId) => 
        _game.ChangeShootingType(connectionId, new ShortRangeShootingAlgorithm());
    
    public void LongShootingAlgorithm(string connectionId) => 
        _game.ChangeShootingType(connectionId, new LongRangeShootingAlgorithm());

    public void ClearMove() =>
        _game.RefreshMoves();

    public void ChangeMap(MapType mapType) =>
        _game.ChangeMap(mapType);

    public Tile[,] GenerateMap() =>
        _game.GenerateMap().Tiles.Invert();

    public Map GetMap() =>
        _game.GetMap();

    public bool ChangeReadyStatus(string connectionId) =>
        _game.ChangeReadyStatus(connectionId);

    public Color FreeColor() =>
        _game.GetFirstAvailableFreeColor();
}