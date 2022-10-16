#region

using Backend.Enums;
using Backend.GameHubs;
using Backend.Utilities;
using Microsoft.AspNetCore.SignalR;

#endregion

namespace Backend.Entities;

public class Player
{
    public string ConnectionID { get; set; }
    public string Name { get; set; }
    public Color Color { get; set; }
    public bool IsReady { get; set; }
    public List<Unit> Units { get; set; }

    public Player(string id, string name, Color color, List<Unit> units)
    {
        ConnectionID = id;
        Name = name;
        Color = color;
        IsReady = false;
        Units = units;
    }

    public async Task Update(Map map)
    {
        await GameHub.Instance.Clients.Client(this.ConnectionID).SendAsync("Map", map.Tiles.Invert());
    }
}