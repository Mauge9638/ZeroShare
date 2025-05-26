using Backend.Models;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface ISnippetService
    {
        Task<SnippetDTO?> GetSnippetAsync(string contentId);
        Task<(string contentId, SnippetDTO snippet)> CreateSnippetAsync(SnippetDTO snippetDTO);

        Task<bool> DeleteSnippetAsync(string contentId);
    }
}