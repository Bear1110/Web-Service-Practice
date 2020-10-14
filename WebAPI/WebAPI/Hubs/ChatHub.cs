using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SomeoneOffline(Player player)
        {
            new GameCenter().removeOnlinePlayer(player);
            await Clients.All.SendAsync("ReceiveOffline", player);
        }
    }
}