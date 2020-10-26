using Microsoft.AspNetCore.SignalR;
using System;
using WebAPI.Models;

namespace WebAPI.Hubs
{
    public class BroadcastHelper
    {
        public static void Online(IHubContext<ChatHub> context, Player player)
        {
            context.Clients.All.SendAsync("ReceiveSomeoneOnline", player);
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
