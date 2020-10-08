using System.Data.Common;

namespace WebAPI.Models
{
    public class Room
    {
        private static int autoIncrement = 0;
        public long Id { get; set; }
        public Player player1 { get; set; }
        public Player player2 { get; set; }

        public Room(Player player)
        {
            Id = autoIncrement++;
            player1 = player;
        }

        public string RoomInfo()
        {

            return "";
        }
    }
}
