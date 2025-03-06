using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NEWZEAL_LAND_WORK_API.Domain_Models;

namespace NEWZEAL_LAND_WORK_API.Data
{
    public class NZwalksDbcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("Server=.;Database=NZwalksDataBase;Trusted_Connection=True;TrustServerCertificate=True; MultipleActiveResultSets=true"));
        }

        public NZwalksDbcontext(DbContextOptions<NZwalksDbcontext> options) : base(options)
        {
        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<Image> images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Region)
                .WithMany()
                .HasForeignKey(w => w.RegionId);

            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Difficulty)
                .WithMany()
                .HasForeignKey(w => w.DifficultyId);
        }
    }
}
