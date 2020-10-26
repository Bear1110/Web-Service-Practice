using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.GameClasses;
using WebAPI.Hubs;
using WebAPI.Models;

namespace WebAPI
{
    public class GameCenter
    {
        private static readonly List<Player> onlinePlayers = new List<Player>();
        private static readonly Dictionary<int,MapInfo> MapInfos = new Dictionary<int, MapInfo>();
        private readonly GameDBContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public GameCenter(GameDBContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        #region Player
        public void AddOnlinePlayer(Player player) => onlinePlayers.Add(player);
        public void RemoveOnlinePlayer(Player player) => onlinePlayers.Remove(onlinePlayers.Single(e => e.Id == player.Id));
        public List<Player> ListOnlinePlayer() => onlinePlayers;
        #endregion

        #region Room

        public async Task<Room> CreateRoom(Player player)
        {
            var temp = new Room(player);
            _context.Rooms.Add(temp);
            await _context.SaveChangesAsync();
            return temp;
        }

        public async Task<Room> JoinRoom(Room room, Player player)
        {
            room.JoinPlayer(player);
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task RemovePlayerFromRoom(int roomId, int playerId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            room.RemovePlayer(playerId);
            if (room.isEmpty())
                _context.Rooms.Remove(room);
            else
                _context.Entry(room).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                System.Diagnostics.Debug.WriteLine("Maybe Not found this room");
            }
        }
        #endregion

        #region Game
        public void StartGame(Room room, string[] map)
        {
            //Now, Player 1 must supply map, Player 2 just attack.
            MapInfo mapInfo = new MapInfo(room, map);
            MapInfos.Add(room.Id, mapInfo);
            BroadcastHelper.BroadcastStartGame(_hubContext, room);
        }

        public void AttackOnMap(Room room,int x, int y)
        {
            MapInfo MapInfo = null;
            MapInfos.TryGetValue(room.Id, out MapInfo);
            string message = MapInfo.Attack(x,y);
            if(MapInfo.GameSet())
                BroadcastHelper.BroadcastGameSet(_hubContext, room);
            else
                BroadcastHelper.BroadcastAttackResult(_hubContext, room, MapInfo.VisualizeMap(), message);
        }
        #endregion
    }
}
