using Vizsgaremek.Entities;
using Microsoft.EntityFrameworkCore;

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

        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

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

        public DbSet<BrickEntity> Bricks { get; set; }
    }
}
