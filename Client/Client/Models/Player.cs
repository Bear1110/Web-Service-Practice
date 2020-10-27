using System;

namespace Client.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public DateTime CreatedDate { get; set; }
        public override string ToString() => string.Format($"ID:{Id}  Name:{Name,-16} IP:{Ip}");
    }
}
