using Microsoft.EntityFrameworkCore;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    public class VegaDbContext : DbContext
    {
        // TODO: Refactor data annotations with Fluent API
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            :base(options)
        {           
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features {get; set;}
    }
}