using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace WebAPI.Models
{
    public class GameDBContext : DbContext
    {
        public GameDBContext(DbContextOptions<GameDBContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(); // for "virtual" work in model
        }

        //table name
        public DbSet<Player> Players { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
