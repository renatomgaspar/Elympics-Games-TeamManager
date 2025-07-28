using Elympics_Games.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elympics_Games.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
