using Backend.Models;
using Backend.Repositories;
using NanoidDotNet;
using System.Text;

namespace Backend.Services
{
    public class SnippetService(ISnippetRepository repository, ILogger<SnippetService> logger) : ISnippetService
    {
        private readonly ISnippetRepository _repository = repository;
        private readonly ILogger<SnippetService> _logger = logger;

        // TODO: Use configuration for max size
        private const int MAX_SIZE_KB = 64;
        private const int MAX_SIZE_BYTES = MAX_SIZE_KB * 1024;
        public async Task<SnippetViewDTO?> GetSnippetAsync(string contentId)
        {
            Snippet? snippet = await _repository.GetByContentIdAsync(contentId);
            if (snippet == null)
            {
                return null;
            }
            if (snippet.BurnAfterRead)
            {
                await _repository.DeleteAsync(contentId);
            }

            return new SnippetViewDTO(snippet);
        }

        public async Task<(string contentId, SnippetDTO snippet)> CreateSnippetAsync(SnippetDTO snippetDTO)
        {

            if (string.IsNullOrEmpty(snippetDTO.Content))
            {
                throw new ArgumentException("Content cannot be empty");
            }

            // Calculate the size of the encrypted content (base64 string)
            var contentSizeBytes = Encoding.UTF8.GetByteCount(snippetDTO.Content);

            if (contentSizeBytes > MAX_SIZE_BYTES)
            {
                var contentSizeKB = contentSizeBytes / 1024.0;
                throw new ArgumentException($"Content size ({contentSizeKB:F1} KB) exceeds maximum allowed size of {MAX_SIZE_KB} KB");
            }

            // Generate a unique ID
            string contentId = Nanoid.Generate(size: 12);

            // Create the entity
            var snippet = new Snippet
            {
                Content = snippetDTO.Content,
                ContentId = contentId,
                IV = snippetDTO.IV,
                BurnAfterRead = snippetDTO.BurnAfterRead,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = snippetDTO.ExpiresAt
            };

            await _repository.CreateAsync(snippet);

            return (contentId, new SnippetDTO(snippet));
        }

        public async Task<bool> DeleteSnippetAsync(string contentId)
        {
            try
            {
                return await _repository.DeleteAsync(contentId);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to delete snippet with ContentId '{ContentId}'", contentId);
                return false;
            }
        }
    }
}