using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Profile> Profiles { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base (options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Profile>()
                .HasKey(e => new { e.UserId});

            builder.Entity<Profile>()
                .HasOne(p => p.User).WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId);

            base.OnModelCreating(builder);
        }
    }
}
