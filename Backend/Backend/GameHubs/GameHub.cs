#region

using Backend.Entities;
using Backend.Enums;
using Backend.Utilities;
using Microsoft.AspNetCore.SignalR;

#endregion

namespace Backend.GameHubs;

public class GameHub : Hub
{
    private readonly Game _game = Game.GetGameInstance();
    private const string GameGroup = "GAME";

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"A user connected. (ConnectionID: {Context.ConnectionId})");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception ex)
    {
        var connectionId = Context.ConnectionId;
        var player = _game.GetPlayerByConnectionId(connectionId);
        if (player is not null)
        {
            _game.RemovePlayer(player);
            await Groups.RemoveFromGroupAsync(connectionId, GameGroup);
            await Clients.Group(GameGroup).SendAsync("PlayerLeave", player);
        }

        Console.WriteLine($"A user ({player?.Name}) disconnected. (ConnectionID: {connectionId}");
        await base.OnDisconnectedAsync(ex);
    }

    public async Task EnterUserName(string name)
    {
        Console.WriteLine("Entered name: " + name);
        var color = _game.GetFirstAvailableFreeColor();
        var player = new Player(Context.ConnectionId, name, color, null);
        player.Units = player.Color switch
        {
            Color.Red => new List<Unit>
            {
                new Tank
                {
                    CurrentHealth = 2,
                    Damage = 1,
                    IsAerial = false,
                    MaxHealth = 2,
                    MovesPerTurn = 1,
                    Color = Color.Red,
                    PosX = 10,
                    PosY = 1
                }
            },
            Color.Blue => new List<Unit>
            {
                new Tank
                {
                    CurrentHealth = 2,
                    Damage = 1,
                    IsAerial = false,
                    MaxHealth = 2,
                    MovesPerTurn = 1,
                    Color = Color.Blue,
                    PosX = 10,
                    PosY = 19
                }
            },
            Color.Green => new List<Unit>
            {
                new Tank
                {
                    CurrentHealth = 2,
                    Damage = 1,
                    IsAerial = false,
                    MaxHealth = 2,
                    MovesPerTurn = 1,
                    Color = Color.Green,
                    PosX = 1,
                    PosY = 10
                }
            },
            Color.Yellow => new List<Unit>
            {
                new Tank
                {
                    CurrentHealth = 2,
                    Damage = 1,
                    IsAerial = false,
                    MaxHealth = 2,
                    MovesPerTurn = 1,
                    Color = Color.Yellow,
                    PosX = 19,
                    PosY = 10
                }
            },
            _ => player.Units
        };
        switch (_game.AddPlayer(player))
        {
            case AddPlayerState.PlayerWithNameExists:
                await Clients.Caller.SendAsync("ConfirmUserName", name,
                    "This username is unavailable. Please choose another one!");
                break;
            case AddPlayerState.ServerIsFull:
                await Clients.Caller.SendAsync("ConfirmUserName", name, "Cannot join the game. The game is full.");
                break;
            case AddPlayerState.GameInProgress:
                await Clients.Caller.SendAsync("ConfirmUserName", name,
                    "Cannot join the game. The game is in progress.");
                break;
            case AddPlayerState.Completed:
            default:
                await Clients.Group(GameGroup).SendAsync("PlayerJoin", player);
                await Groups.AddToGroupAsync(Context.ConnectionId, GameGroup);
                await Clients.Caller.SendAsync("ConfirmUserName", _game.GetPlayers(), "", player);
                break;
        }
    }

    public async Task SendMove(int move)
    {
        var (oldX, oldY, newX, newY) = _game.MoveItem(Context.ConnectionId, move);
        if (oldX == -1)
        {
            return;
        }

        await Clients.Group(GameGroup).SendAsync("MoveItem", oldX, oldY, newX, newY);
        await Clients.Group(GameGroup).SendAsync("NextTurn", Context.ConnectionId, _game.NextPlayer());
    }

    public async Task ReadyUp()
    {
        var connectionId = Context.ConnectionId;
        var ready = _game.ChangeReadyStatus(connectionId);
        await Clients.Group(GameGroup).SendAsync("ReadyStatus", connectionId, ready);
        if (Game.IsGameStarting)
        {
            await Clients.Group(GameGroup).SendAsync("Map", _game.GenerateMap().Tiles);
            await Clients.Group(GameGroup).SendAsync("FirstTurn", _game.NextPlayer());
            await Clients.Group(GameGroup).SendAsync("GameStatus", true);
        }
    }
}