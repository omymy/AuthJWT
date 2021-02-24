using Microsoft.EntityFrameworkCore;

namespace AuthJWT
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        }
    }
}
