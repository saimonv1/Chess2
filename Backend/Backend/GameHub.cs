using Microsoft.AspNetCore.SignalR;

namespace Backend
{
    public class GameHub : Hub
    {
        private static Dictionary<string, string> userNames = new Dictionary<string, string>();

        public async Task Connect()
        {
            Console.WriteLine($"A user connected. (ConnectionID: {Context.ConnectionId})");
        }

        public async Task OnDisconnected()
        {
            var userName = userNames[Context.ConnectionId];
            userNames.Remove(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "game");
            Console.WriteLine($"A user ({userName}) disconnected. (ConnectionID: {Context.ConnectionId}");
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
            await Groups.AddToGroupAsync(Context.ConnectionId, "game");
            await Clients.Caller.SendAsync("ConfirmUserName", name, "");
        }
    }
}
