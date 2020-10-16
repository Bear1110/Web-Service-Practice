namespace Client.Models
{
    public class Room
    {
        public long Id { get; set; }
        public Player player1 { get; set; }
        public Player player2 { get; set; }

        public override string ToString() => string.Format($"ID:{Id}  Player1:{player1.Name,-16} Player2:{player2?.Name,-16}");
    }
}
