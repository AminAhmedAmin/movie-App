using Microsoft.EntityFrameworkCore;

namespace moveis.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Movie> movies { get; set; }
        public DbSet<Ganara> Ganaras { get; set; }

    }
}
