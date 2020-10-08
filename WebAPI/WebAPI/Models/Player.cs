using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Models
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
    }
}
