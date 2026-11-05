using Microsoft.EntityFrameworkCore;
using Warehouse.Entites;
using Warehouse.Entities;
using WarehouseEntity = Warehouse.Entites.Warehouse;

namespace Warehouse.Database
{
    public class WarehouseDbContext : DbContext
    {
        public DbSet<WarehouseEntity> Warehouses => Set<WarehouseEntity>();
        public DbSet<Cabinet> Cabinets => Set<Cabinet>();
        public DbSet<Shelf> Shelves => Set<Shelf>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockMovement>()
                .HasOne(x => x.FromShelf)
                .WithMany(x => x.OutgoingMovements)
                .HasForeignKey(x => x.FromShelfId);

            modelBuilder.Entity<StockMovement>()
                .HasOne(x => x.ToShelf)
                .WithMany(x => x.IncomingMovements)
                .HasForeignKey(x => x.ToShelfId);

            modelBuilder.Entity<Shelf>()
                .HasOne(x => x.Cabinet)
                .WithMany(x => x.Shelves)
                .HasForeignKey(x => x.CabinetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stock>()
                .HasOne(x => x.Shelf)
                .WithMany(x => x.Stocks)
                .HasForeignKey(x => x.ShelfId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stock>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Stocks)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
