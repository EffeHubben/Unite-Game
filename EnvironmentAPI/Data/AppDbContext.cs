using Microsoft.EntityFrameworkCore;
using EnvironmentAPI.Models;

namespace EnvironmentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<World> Worlds => Set<World>();
        public DbSet<WorldObject> WorldObjects => Set<WorldObject>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorldObject>().OwnsOne(p => p.Position);
            modelBuilder.Entity<WorldObject>().OwnsOne(p => p.Scale);
            modelBuilder.Entity<WorldObject>().OwnsOne(p => p.Rotation);
        }
    }
}
