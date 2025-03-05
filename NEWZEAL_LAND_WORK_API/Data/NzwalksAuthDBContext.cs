using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NEWZEAL_LAND_WORK_API.Data
{
    public class NzwalksAuthDBContext : IdentityDbContext
    {
        public NzwalksAuthDBContext(DbContextOptions <NzwalksAuthDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleID = "c0a741be-2d91-40c5-814a-f3f6e21d7163";
            var writerRoleID = "a61b12e1-e1db-4ad8-a3ee-c2ad17552bd2";

            var roles = new List<IdentityRole>
            {
                new IdentityRole {
                    Id = readerRoleID,
                    ConcurrencyStamp = readerRoleID,
                    Name = "Reder",
                    NormalizedName = "Reader".ToUpper(),

                },

                new IdentityRole {
                    Id = writerRoleID,
                    ConcurrencyStamp = writerRoleID,
                    Name = "Writer",
                    NormalizedName = "WRITER".ToUpper(),
                    
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
