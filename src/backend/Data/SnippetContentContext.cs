using Microsoft.EntityFrameworkCore;
using Backend.Models;
namespace Backend.Data
{
    public class SnippetContentContext : DbContext
    {
        public SnippetContentContext(DbContextOptions<SnippetContentContext> options)
            : base(options)
        {
        }

        public DbSet<SnippetContent> SnippetContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnippetContent>()
                .Property(s => s.Content)
                .IsRequired();

            modelBuilder.Entity<SnippetContent>()
                .Property(s => s.ContentId)
                .IsRequired();

            modelBuilder.Entity<SnippetContent>()
                .Property(s => s.IV)
                .IsRequired();

            modelBuilder.Entity<SnippetContent>()
                .Property(s => s.BurnAfterRead)
                .HasDefaultValue(false);

            modelBuilder.Entity<SnippetContent>()
                .Property(s => s.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<SnippetContent>()
                .Property(s => s.ExpiresAt)
                .IsRequired(false);
        }
    }
}