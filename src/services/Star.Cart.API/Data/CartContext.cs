using Microsoft.EntityFrameworkCore;
using Star.Cart.API.Models;
using System.Linq;

namespace Star.Cart.API.Data
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartClient> CartClients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Entity<CartClient>()
                .HasIndex(c => c.ClientId)
                .HasDatabaseName("IDX_Client");

            modelBuilder.Entity<CartClient>()
              .HasMany(c => c.Items)
              .WithOne(i => i.CartClient)
              .HasForeignKey(c => c.CartId);
        }
    }
}
