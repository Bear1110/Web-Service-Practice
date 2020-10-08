using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI
{
    public class GameCenter
    {
        private static readonly List<Room> rooms = new List<Room>();
        private readonly PlayerContext _context;

        public GameCenter(PlayerContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetRooms()
        {
            return rooms.ToList();
        }

        public Room CreateRoom(Player player)
        {
            Room temp = new Room(player);
            rooms.Add(temp);
            return temp;
        }

        public Room JoinRoom(int roomid, Player player)
        {
            var room = rooms.Find(room => room.Id == roomid);
            if(room != null)
                room.player2 = player;
            return room;
        }
    }
}
