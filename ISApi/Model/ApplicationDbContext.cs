using Microsoft.EntityFrameworkCore;

namespace ISApi.Model
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
