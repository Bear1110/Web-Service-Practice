using Microsoft.AspNetCore.SignalR;
using WebAPI.Models;

namespace WebAPI.Hubs
{
    public class BroadcastHelper
    {
        public static void Online(IHubContext<ChatHub> context, Player player)
        {
            context.Clients.All.SendAsync("ReceiveSomeoneOnline", player);
        }
    }
}
