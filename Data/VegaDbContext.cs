using Microsoft.EntityFrameworkCore;
using VegaSPA.Models;

namespace VegaSPA.Data
{
    // TODO: Pluggable data tier
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            :base(options)
        {           
        }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite primary key definition
            modelBuilder.Entity<VehicleFeature>()
                .HasKey(vf => new {vf.VehicleId, vf.FeatureId});
            // GetDate runs on server side.
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.LastModified)
                .HasDefaultValueSql("[dbo].[getcurrentdate]()");
        }
    }
}