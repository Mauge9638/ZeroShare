using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class SnippetRepository(SnippetContext context) : ISnippetRepository
    {
        private readonly SnippetContext _context = context;

        public async Task<Snippet?> GetByContentIdAsync(string contentId)
        {
            return await _context.Snippet
                .FirstOrDefaultAsync(s => s.ContentId == contentId &&
                                          (s.ExpiresAt == null || s.ExpiresAt > DateTime.UtcNow));
        }

        public async Task<Snippet> CreateAsync(Snippet snippet)
        {
            _context.Snippet.Add(snippet);
            await _context.SaveChangesAsync();
            return snippet;
        }

        public async Task<bool> DeleteAsync(string contentId)
        {
            var snippet = await GetByContentIdAsync(contentId);
            if (snippet == null) return false;

            _context.Snippet.Remove(snippet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> DeleteExpiredSnippetsAsync(DateTime threshold)
        {
            var expiredSnippets = await _context.Snippet
                .Where(s => s.ExpiresAt.HasValue && s.ExpiresAt < threshold)
                .ToListAsync();

            if (expiredSnippets.Count <= 0) return 0;

            _context.Snippet.RemoveRange(expiredSnippets);
            await _context.SaveChangesAsync();
            return expiredSnippets.Count;
        }
    }
}