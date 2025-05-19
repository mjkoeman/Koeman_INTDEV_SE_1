using Microsoft.EntityFrameworkCore;
using Koeman_INTDEV_SE_1.Models;    

namespace Koeman_INTDEV_SE_1.Data
{
    public class DbContextINTDEV1 : DbContext
    {
        public DbContextINTDEV1(DbContextOptions<DbContextINTDEV1> options) : base(options) 
        { 
        } 

        public DbSet<Klant> Klanten { get; set; }   
        public DbSet<Product> Producten { get; set; }   
        public DbSet<Bestelling> Bestellingen { get; set; }
        public DbSet<BestellingProduct> BestellingProducten { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BestellingProduct>()
                .HasKey(bp => new { bp.BestellingId, bp.ProductId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
