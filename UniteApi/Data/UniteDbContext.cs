using Microsoft.EntityFrameworkCore;
using UniteApi.Models;

namespace UniteApi.Data
{
    public class UniteDbContext : DbContext
    {
        public UniteDbContext(DbContextOptions<UniteDbContext> options) : base(options) { }

        public DbSet<WorldEntity> Worlds { get; set; }
        public DbSet<WorldObjectEntity> WorldObjects { get; set; }
    }
}
