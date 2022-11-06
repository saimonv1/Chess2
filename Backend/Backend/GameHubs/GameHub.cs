#region

using Backend.Entities;
using Backend.Enums;
using Backend.Utilities;
using Backend.Utilities.Command;
using Microsoft.AspNetCore.SignalR;

#endregion

namespace Backend.GameHubs;

public class GameHub : Hub
{
    public static GameHub Instance { get; private set; }

    private readonly Game _game = Game.GetGameInstance();
    private const string GameGroup = "GAME";
    //private readonly MoveCommand _moveCommand = new MoveCommand();
    private Mover Mover = new Mover();

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
        int moves = 0;

        switch (move)
        {
            case 0:
                moves = Mover.ExecuteCommand(new MoveUpCommand(), Context.ConnectionId);
                break;
            case 1:
                moves = Mover.ExecuteCommand(new MoveRightCommand(), Context.ConnectionId);
                break;
            case 2:
                moves = Mover.ExecuteCommand(new MoveDownCommand(), Context.ConnectionId);
                break;
            case 3:
                moves = Mover.ExecuteCommand(new MoveLeftCommand(), Context.ConnectionId);
                break;
            default:
                break;
        }

        //var moves = _moveCommand.Execute(move, Context.ConnectionId);

        await Clients.Group(GameGroup).SendAsync("MovesUpdate", moves);
        
        if(moves != 0)
        {
            return;
        }

        Mover.Clear();

        _game.RefreshMoves();
        //_moveCommand.ClearHistory();

        //await Clients.Group(GameGroup).SendAsync("MoveItem", oldY, oldX, newY, newX);
        await Clients.Group(GameGroup).SendAsync("NextTurn", Context.ConnectionId, _game.NextPlayer());
        await Clients.Group(GameGroup).SendAsync("MovesUpdate", 3);
    }

    public async Task MapChange(MapType type)
    {
        _game.ChangeMap(type);
        _game.RefreshMoves();
    }

    public async Task Undo()
    {
        //var moves = _moveCommand.Undo(Context.ConnectionId);
        var moves = Mover.UndoCommand(Context.ConnectionId);
        await Clients.Group(GameGroup).SendAsync("MovesUpdate", moves);
    }

    public async Task ReadyUp()
    {
        var connectionId = Context.ConnectionId;
        var ready = _game.ChangeReadyStatus(connectionId);
        await Clients.Group(GameGroup).SendAsync("ReadyStatus", connectionId, ready);
        if (Game.IsGameStarting)
        {
            await Clients.Group(GameGroup).SendAsync("MovesUpdate", 3);
            await Clients.Group(GameGroup).SendAsync("Map", _game.GenerateMap().Tiles.Invert());
            await Clients.Group(GameGroup).SendAsync("FirstTurn", _game.NextPlayer());
            await Clients.Group(GameGroup).SendAsync("GameStatus", true);
        }
    }
}