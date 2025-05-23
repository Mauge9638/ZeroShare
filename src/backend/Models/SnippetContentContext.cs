using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class SnippetContentContext : DbContext
    {
        public SnippetContentContext(DbContextOptions<SnippetContentContext> options)
            : base(options)
        {
        }

        public DbSet<SnippetContent> SnippetContents { get; set; }
    }
}