using Microsoft.EntityFrameworkCore;
using Vega.Models;

namespace Vega.Persistence
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Make> Make { get; set; } 
        public DbSet<Feature> Features  { get; set; }   
        public DbSet<Model> Models  { get; set; }   
        public DbSet<Vehicle> vehichles  { get; set; }  

        public DbSet<Photo> Photos  { get; set; }    
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
         : base(options)
        {

        }
            
         protected override void OnModelCreating(ModelBuilder modelBuilder) 
         {
           modelBuilder.Entity<VehicleFeature>().HasKey(
               VehicleFeature=>new {VehicleFeature.VehicleId, VehicleFeature.FeatureId});
         }
    }
}