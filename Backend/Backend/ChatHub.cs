using Microsoft.AspNetCore.SignalR;

namespace Backend
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
            => await Clients.All.SendAsync("ReceiveMessage", user, message);

        public async Task JoinRoom(RoomData data)
        {
            Console.WriteLine($"{data.User} joined room {data.Room}");
        }
    }
    public class RoomData
    {
        public string User { get; set; }
        public string Room { get; set; }
    }
}
