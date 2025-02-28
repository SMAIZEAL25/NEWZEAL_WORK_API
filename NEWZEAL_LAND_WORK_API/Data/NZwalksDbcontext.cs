using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NEWZEAL_LAND_WORK_API.Domain_Models;

namespace NEWZEAL_LAND_WORK_API.Data
{
    public class NZwalksDbcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("Server=.;Database=StudentManagementAPI;Trusted_Connection=True;TrustServerCertificate=True; MultipleActiveResultSets=true"));
        }

        public NZwalksDbcontext(DbContextOptions<NZwalksDbcontext> options): base(options)
        {
        }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Difficulty> Difficulties { get; set; }
    }
}
