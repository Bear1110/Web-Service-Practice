namespace Client.Models
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }

        public override string ToString() => string.Format($"ID:{Id}  Name:{Name,-16 } IP:{IP}");
    }
}
