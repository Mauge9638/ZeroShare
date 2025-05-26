using Backend.Models;
using Backend.Repositories;
using Microsoft.Extensions.Logging;
using NanoidDotNet;
using System;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SnippetService(ISnippetRepository repository, ILogger<SnippetService> logger) : ISnippetService
    {
        private readonly ISnippetRepository _repository = repository;
        private readonly ILogger<SnippetService> _logger = logger;

        public async Task<SnippetDTO?> GetSnippetAsync(string contentId)
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

            return new SnippetDTO(snippet);
        }

        public async Task<(string contentId, SnippetDTO snippet)> CreateSnippetAsync(SnippetDTO snippetDTO)
        {
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

            // Save to database
            await _repository.CreateAsync(snippet);

            // Return the result
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