using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly GameCenter _gamecenter;

        public ChatHub(
            GameCenter gameCenter)
        {
            _gamecenter = gameCenter;
        }
        //Function Name is the actually monitor event for broadcast
        public async Task SomeoneOffline(Player player)
        {
            _gamecenter.RemoveOnlinePlayer(player);
            await Clients.All.SendAsync("ReceiveOffline", player);
        }

        #region room
        //Like a group
        public async Task JoinToRoom(Player player, Room room )
        {
            string roomId = room.Id.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            //System.Diagnostics.Debug.WriteLine($"{Context.ConnectionId} has joined the room {roomId}.");
            await Clients.Group(roomId).SendAsync("ReceiveSomeoneJoinRoom", player);
        }

        public async Task LeaveFromRoom(Player player, Room room)
        {
            await _gamecenter.RemovePlayerFromRoom(room.Id, player.Id);
            string roomId = room.Id.ToString();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            //System.Diagnostics.Debug.WriteLine($"{Context.ConnectionId} has left the room {roomId}.");
            await Clients.Group(roomId).SendAsync("ReceiveLeaveJoinRoom", player);
        }
        #endregion

        #region gameCommunicate
        public void Attack(Room room, int x, int y)
        {
            _gamecenter.AttackOnMap(room,x,y);
        }
        #endregion
    }
}