using indasYemek.Entities;
using Microsoft.EntityFrameworkCore;

namespace indasYemek.context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }


        const string connectionString = "SQL SERVER CONNECTION STRING";
        public DbSet<yemekListesi> yemekListesi { get; set; }
        public DbSet<istekListesi> istekListesi { get; set; }
        public DbSet<deviceTable> deviceTable { get; set; }
        public DbSet<userTable > userTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<yemekListesi>(entity =>
            {
                entity.HasKey(a => a.id);
            }
            );

            modelBuilder.Entity<istekListesi>(entity =>
            {
                entity.HasKey(a => a.id);
            }
            );

            modelBuilder.Entity<deviceTable>(entity =>
            {
                entity.HasKey(a=> a.id);
            }
            );
        }

    }
}
