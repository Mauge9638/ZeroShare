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

        public async Task<int> DeleteInactiveSnippetsAsync(int inactivityRetentionDays)
        {
            var expiredSnippets = await _context.Snippet
                .Where(s => s.LastAccessedAt.AddDays(inactivityRetentionDays) < DateTime.UtcNow)
                .ToListAsync();

            if (expiredSnippets.Count <= 0) return 0;

            _context.Snippet.RemoveRange(expiredSnippets);
            await _context.SaveChangesAsync();
            return expiredSnippets.Count;
        }

        public async Task<Snippet?> UpdateAsync(Snippet snippet)
        {
            var existingSnippet = await GetByContentIdAsync(snippet.ContentId);
            if (existingSnippet == null) return null;

            existingSnippet.Content = snippet.Content;
            existingSnippet.IV = snippet.IV;
            existingSnippet.BurnAfterRead = snippet.BurnAfterRead;
            existingSnippet.ExpiresAt = snippet.ExpiresAt;
            existingSnippet.LastAccessedAt = DateTime.UtcNow;

            _context.Snippet.Update(existingSnippet);
            await _context.SaveChangesAsync();
            return existingSnippet;
        }

        public async Task<Snippet?> UpdateLastAccessedAsync(string contentId, DateTime lastAccessedAt)
        {
            var snippet = await GetByContentIdAsync(contentId);
            if (snippet == null) return null;

            snippet.LastAccessedAt = lastAccessedAt;
            _context.Snippet.Update(snippet);
            await _context.SaveChangesAsync();
            return snippet;
        }
    }
}