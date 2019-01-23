using Microsoft.EntityFrameworkCore;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            :base(options)
        {           
        }

        public DbSet<Make> Makes { get; set; }
    }
}