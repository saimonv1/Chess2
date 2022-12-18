#region

using Backend.Entities;
using Backend.Enums;
using Backend.Proxy;
using Backend.Utilities;
using Backend.Utilities.Command;
using Backend.Utilities.Facade;
using Backend.Utilities.Interpreter;
using Backend.Utilities.Interpreter.Enums;
using Microsoft.AspNetCore.SignalR;

#endregion

namespace Backend.GameHubs;

public class GameHub : Hub
{
    public static GameHub Instance { get; private set; }

    private readonly BestFacade _facade = new();

    private AddPlayerProxy _addPlayerProxy = new();

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
        switch (_addPlayerProxy.AddPlayer(_facade, player))
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

    public async Task SendMove(int move, int amount = 1)
    {
        var moves = 0;

        for (int i = 0; i < amount; i++)
        {
            moves = move switch
            {
                0 => Mover.ExecuteCommand(new MoveUpCommand(), Context.ConnectionId),
                1 => Mover.ExecuteCommand(new MoveRightCommand(), Context.ConnectionId),
                2 => Mover.ExecuteCommand(new MoveDownCommand(), Context.ConnectionId),
                3 => Mover.ExecuteCommand(new MoveLeftCommand(), Context.ConnectionId),
                _ => 0
            };
        }

        await Clients.Group(GameGroup).SendAsync("MovesUpdate", moves);
        await Clients.Group(GameGroup).SendAsync("PickupsUpdate", _facade.GetMap().PickupsCount());

        if (moves != 0)
        {
            return;
        }

        Mover.Clear();

        _facade.ClearMove(Context.ConnectionId);
        await Clients.Group(GameGroup).SendAsync("NextTurn", Context.ConnectionId, _facade.NextPlayer());
        await Clients.Group(GameGroup).SendAsync("MovesUpdate", 3);
    }

    public async Task Shoot(int move)
    {
        _facade.Shoot(Context.ConnectionId, move);
        Mover.Clear();

        _facade.ClearMove(Context.ConnectionId);

        if (_facade.IsGameOver())
        {
            await Clients.Group(GameGroup).SendAsync("GameOver", _facade.GetWinnerName());
            _facade.GetPlayers().ForEach(async x => await Groups.RemoveFromGroupAsync(x.ConnectionID, GameGroup));
            _facade.ClearPlayers();
            return;
        }
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
        _facade.ClearMove(Context.ConnectionId);
    }

    public async Task Undo()
    {
        var moves = Mover.UndoCommand(Context.ConnectionId);
        await Clients.Group(GameGroup).SendAsync("MovesUpdate", moves);
    }

    public async Task MapRevert()
    {
        _facade.MapRevert();
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
            await Clients.Group(GameGroup).SendAsync("PickupsUpdate", _facade.GetMap().PickupsCount());
            await Clients.Group(GameGroup).SendAsync("InvalidCommand", false);
        }
    }

    public async Task Interpret(string textCommand)
    {
        var command = Interpreter.Interpret(textCommand);

        var shoot = command as InterpretedShootCommand;
        var move = command as InterpretedMoveCommand;

        if (shoot is not null)
        {
            switch (shoot.length)
            {
                case Length.Short:
                    await ShortShooting();
                    break;
                case Length.Long:
                    await LongShooting();
                    break;
            }

            switch (shoot.direction)
            {
                case Direction.Up:
                    await Shoot(0);
                    break;
                case Direction.Right:
                    await Shoot(1);
                    break;
                case Direction.Down:
                    await Shoot(2);
                    break;
                case Direction.Left:
                    await Shoot(3);
                    break;
            }

            await Clients.Caller.SendAsync("InvalidCommand", false);
            return;
        }

        if (move is not null)
        {
            switch (move.direction)
            {
                case Direction.Up:
                    await SendMove(0, move.amount);
                    break;
                case Direction.Right:
                    await SendMove(1, move.amount);
                    break;
                case Direction.Down:
                    await SendMove(2, move.amount);
                    break;
                case Direction.Left:
                    await SendMove(3, move.amount);
                    break;
            };

            await Clients.Caller.SendAsync("InvalidCommand", false);
            return;
        }

        await Clients.Caller.SendAsync("InvalidCommand", true);
    }
}