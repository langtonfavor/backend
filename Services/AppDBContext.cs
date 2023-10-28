using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username);
                    //.IsRequired(false); // This allows null values for Username
                // Other property configurations...
            });
        }
    }
}
