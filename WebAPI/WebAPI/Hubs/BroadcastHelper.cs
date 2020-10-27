using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Hubs
{
    public class BroadcastHelper
    {
        public static void Online(IHubContext<ChatHub> hubContext, Player player)
        {
            hubContext.Clients.All.SendAsync("ReceiveSomeoneOnline", player);
            hubContext.Clients.All.SendAsync("TestForCpp", JsonSerializer.Serialize(player));
        }

        public static void BroadcastAttackResult(IHubContext<ChatHub> hubContext, Room room, string[] map, string message)
        {
            hubContext.Clients.Group(room.Id.ToString()).SendAsync("ReceiveAttackResult", map, message);
        }

        public static void BroadcastGameSet(IHubContext<ChatHub> hubContext, Room room)
        {
            hubContext.Clients.Group(room.Id.ToString()).SendAsync("ReceiveGameSet");
        }

        public static void BroadcastStartGame(IHubContext<ChatHub> hubContext, Room room)
        {
            hubContext.Clients.Group(room.Id.ToString()).SendAsync("ReceiveStartGame");
        }
    }
}
