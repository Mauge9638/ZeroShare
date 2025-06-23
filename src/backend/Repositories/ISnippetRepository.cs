using Backend.Models;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public interface ISnippetRepository
    {
        Task<Snippet?> GetByContentIdAsync(string contentId);
        Task<Snippet> CreateAsync(Snippet snippet);
        Task<bool> DeleteAsync(string contentId);
        Task<int> DeleteExpiredSnippetsAsync(DateTime threshold);
        Task<int> DeleteInactiveSnippetsAsync(int inactivityRetentionDays);
        Task<Snippet?> UpdateAsync(Snippet snippet);
        Task<Snippet?> UpdateLastAccessedAsync(string contentId, DateTime lastAccessedAt);
    }
}