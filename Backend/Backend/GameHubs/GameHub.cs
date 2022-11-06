#region

using Backend.Entities;
using Backend.Enums;
using Backend.Utilities;
using Backend.Utilities.Command;
using Backend.Utilities.Facade;
using Microsoft.AspNetCore.SignalR;

#endregion

namespace Backend.GameHubs;

public class GameHub : Hub
{
    public static GameHub Instance { get; private set; }
    
    private readonly BestFacade _facade = new();

    private const string GameGroup = "GAME";

    //private readonly MoveCommand _moveCommand = new MoveCommand();
    private readonly Mover Mover = new();

    public GameHub()
    {
        Instance = this;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"A user connected. (ConnectionID: {Context.ConnectionId})");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        var connectionId = Context.ConnectionId;
        var player = _facade.GetPlayer(connectionId);
        if (player is not null)
        {
            _facade.RemovePlayer(player);
            await Groups.RemoveFromGroupAsync(connectionId, GameGroup);
            await Clients.Group(GameGroup).SendAsync("PlayerLeave", player);
        }

        Console.WriteLine($"A user ({player?.Name}) disconnected. (ConnectionID: {connectionId}");
        await base.OnDisconnectedAsync(ex);
    }

    public async Task EnterUserName(string name)
    {
        Console.WriteLine("Entered name: " + name);
        var color = _facade.FreeColor();
        var player = new Player(Context.ConnectionId, name, color, null);
        switch (_facade.AddPlayer(player))
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
                await Clients.Caller.SendAsync("ConfirmUserName", _facade.GetPlayers(), "", player);
                break;
        }
    }

    public async Task SendMove(int move)
    {
        var moves = move switch
        {
            0 => Mover.ExecuteCommand(new MoveUpCommand(), Context.ConnectionId),
            1 => Mover.ExecuteCommand(new MoveRightCommand(), Context.ConnectionId),
            2 => Mover.ExecuteCommand(new MoveDownCommand(), Context.ConnectionId),
            3 => Mover.ExecuteCommand(new MoveLeftCommand(), Context.ConnectionId),
            _ => 0
        };

        await Clients.Group(GameGroup).SendAsync("MovesUpdate", moves);

        if (moves != 0)
        {
            return;
        }

        Mover.Clear();

        _facade.ClearMove();
        await Clients.Group(GameGroup).SendAsync("NextTurn", Context.ConnectionId, _facade.NextPlayer());
        await Clients.Group(GameGroup).SendAsync("MovesUpdate", 3);
    }

    public async Task Shoot(int move)
    {
        _facade.Shoot(Context.ConnectionId, move);
        Mover.Clear();

        _facade.ClearMove();
        await Clients.Group(GameGroup).SendAsync("NextTurn", Context.ConnectionId, _facade.NextPlayer());
        await Clients.Group(GameGroup).SendAsync("MovesUpdate", 3);
    }

    public async Task ShortShooting() => 
        _facade.ShortShootingAlgorithm(Context.ConnectionId);

    public async Task LongShooting() => 
        _facade.LongShootingAlgorithm(Context.ConnectionId);

    public async Task MapChange(MapType type)
    {
        _facade.ChangeMap(type);
        _facade.ClearMove();
    }

    public async Task Undo()
    {
        var moves = Mover.UndoCommand(Context.ConnectionId);
        await Clients.Group(GameGroup).SendAsync("MovesUpdate", moves);
    }

    public async Task ReadyUp()
    {
        var connectionId = Context.ConnectionId;
        var ready = _facade.ChangeReadyStatus(connectionId);
        await Clients.Group(GameGroup).SendAsync("ReadyStatus", connectionId, ready);
        if (Game.IsGameStarting)
        {
            await Clients.Group(GameGroup).SendAsync("MovesUpdate", 3);
            await Clients.Group(GameGroup).SendAsync("Map", _facade.GenerateMap());
            await Clients.Group(GameGroup).SendAsync("FirstTurn", _facade.NextPlayer());
            await Clients.Group(GameGroup).SendAsync("GameStatus", true);
        }
    }
}