namespace Client.Models
{
    public class Room
    {
        public int Id { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }


        public override string ToString() => string.Format($"ID:{Id}  Player1:{Player1?.Name,-16} Player2:{Player2?.Name,-16}");
    }
}
