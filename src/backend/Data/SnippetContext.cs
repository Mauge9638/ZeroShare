using Microsoft.EntityFrameworkCore;
using Backend.Models;
namespace Backend.Data
{
    public class SnippetContext(DbContextOptions<SnippetContext> options) : DbContext(options)
    {
        public DbSet<Snippet> Snippet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Snippet>()
                .Property(s => s.Content)
                .IsRequired();

            modelBuilder.Entity<Snippet>()
                .Property(s => s.ContentId)
                .IsRequired();

            modelBuilder.Entity<Snippet>()
                .Property(s => s.IV)
                .IsRequired();

            modelBuilder.Entity<Snippet>()
                .Property(s => s.BurnAfterRead)
                .HasDefaultValue(false);

            modelBuilder.Entity<Snippet>()
                .Property(s => s.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Snippet>()
                .Property(s => s.ExpiresAt)
                .IsRequired(false);
        }
    }
}