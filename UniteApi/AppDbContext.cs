using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using UniteApi.Models;

namespace UniteApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WorldEntity> Worlds { get; set; }
        public DbSet<WorldObjectEntity> WorldObjects { get; set; }

        // Voeg deze regel toe:
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorldObjectEntity>()
                .OwnsOne(w => w.Position);

            modelBuilder.Entity<WorldObjectEntity>()
                .OwnsOne(w => w.Scale);

            modelBuilder.Entity<WorldObjectEntity>()
                .OwnsOne(w => w.Rotation);
        }
    }
}
