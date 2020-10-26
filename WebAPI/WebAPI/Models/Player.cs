using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }

        public DateTime CreatedDate { get; set; }

        public Player() {}

        public Player(string Name)
        {
            this.Name = Name;
            this.CreatedDate = DateTime.UtcNow;
        }
    }
}
