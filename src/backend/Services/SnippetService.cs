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

        public async Task<SnippetViewDTO?> GetSnippetAsync(string contentId)
        {
            Snippet? snippet = await _repository.GetByContentIdAsync(contentId);
            if (snippet == null)
            {
                return null;
            }

            // Set LastAccessedAt to now
            await _repository.UpdateLastAccessedAsync(snippet.ContentId, DateTime.UtcNow);

            if (snippet.BurnAfterRead)
            {
                await _repository.DeleteAsync(contentId);
            }

            return new SnippetViewDTO(snippet);
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

            await _repository.CreateAsync(snippet);

            return (contentId, new SnippetDTO(snippet));
        }


    }
}