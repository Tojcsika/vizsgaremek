using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Entities;

namespace Vizsgaremek.Data
{
    public class DatabaseContext : DbContext
    {
        public static DbContextOptions<DatabaseContext> Options; 
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {
            Options = opt;
        }

        public DatabaseContext(): base(Options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StorageRack>()
                .HasOne(sr => sr.Storage)
                .WithMany(s => s.StorageRacks)
                .HasForeignKey(sr => sr.StorageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shelf>()
                .HasOne(s => s.StorageRack)
                .WithMany(sr => sr.Shelves)
                .HasForeignKey(s => s.StorageRackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ShelfProducts)
                .WithOne(sp => sp.Product)
                .HasForeignKey(sp => sp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShelfProduct>()
                .HasOne(sp => sp.Shelf)
                .WithMany(s => s.ShelfProducts)
                .HasForeignKey(sp => sp.ShelfId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync();
        }

        public DbSet<Storage> Storages { get; set; }
        public DbSet<StorageRack> StorageRacks { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShelfProduct> ShelfProducts { get; set; }
    }
}
