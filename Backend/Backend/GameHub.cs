using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Backend
{
    public class GameHub : Hub
    {
        private static Dictionary<string, string> userNames = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"A user connected. (ConnectionID: {Context.ConnectionId})");
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception ex)
        {
            var connectionId = Context.ConnectionId;
            if (userNames.ContainsKey(connectionId))
            {
                var userName = userNames[connectionId];
                userNames.Remove(connectionId);
                await Groups.RemoveFromGroupAsync(connectionId, "game");
                Console.WriteLine($"A user ({userName}) disconnected. (ConnectionID: {connectionId}");
            }
            else
            {
                Console.WriteLine($"A user disconnected. (ConnectionID: {connectionId}");
            }
            await base.OnDisconnectedAsync(ex);
        }

        public async Task EnterUserName(string name)
        {
            Console.WriteLine("Entered name: " + name);
            if (userNames.ContainsValue(name))
            {
                await Clients.Caller.SendAsync("ConfirmUserName", name, "This username is unvalaibale. Please choose another one!");
                return;
            }
            if(userNames.Count >= 4)
            {
                await Clients.Caller.SendAsync("ConfirmUserName", name, "Cannot join the game. The game is full.");
                return;
            }
            userNames.Add(Context.ConnectionId, name);

            var player = new Player(Context.ConnectionId, name, Color.Blue, null);

            Game.ConnectedPlayers.Add(player);
            await Groups.AddToGroupAsync(Context.ConnectionId, "game");
            await Clients.Caller.SendAsync("ConfirmUserName", player);
            // await Clients.Caller.SendAsync("Color", );
        }
    }
}
