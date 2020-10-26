using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Rooms")]
    public class Room
    {
        public int Id { get; set; }
        public virtual Player Player1 { get; set; }
        public virtual Player Player2 { get; set; }
        public Room() {}
        public Room(Player player)
        {
            Player1 = player;
        }

        public bool isEmpty()
        {
            return Player1 == null && Player2 == null;
        }
        public bool isFull()
        {
            return Player1 != null && Player2 != null;
        }

        public void JoinPlayer(Player player)
        {
            if (Player1 == null)
                Player1 = player;
            else
                Player2 = player;
        }

        public void RemovePlayer(int playerId)
        {
            if (Player1 != null)
            {
                Player1 = Player1.Id == playerId ? null : Player1;
            }
            if (Player2 != null)
            {
                Player2 = Player2.Id == playerId ? null : Player2;
            }
        }
    }
}
