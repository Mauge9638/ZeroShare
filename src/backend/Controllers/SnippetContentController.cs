using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;
using NanoidDotNet;

namespace Backend.Controllers;


[ApiController]
[Route("[controller]")]
public class SnippetContentController : ControllerBase
{
    private static readonly string dummyContent = "This is a dummy content for testing purposes.";

    private readonly ILogger<SnippetContentController> _logger;
    private readonly SnippetContentContext _context;

    public SnippetContentController(ILogger<SnippetContentController> logger, SnippetContentContext context)
    {
        _context = context;
        _logger = logger;
    }


    [HttpGet(Name = "GetSnippetContent")]
    public ActionResult<SnippetContentDTO> GetSnippetContent()
    {
        // Simulate fetching data from a database
        var snippetContent = new SnippetContent
        {
            Content = dummyContent,
            ContentId = "12345",
            IV = "IV12345",
            BurnAfterRead = false,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(1)
        };

        var snippetContentDTO = new SnippetContentDTO(snippetContent);
        return Ok(snippetContentDTO);
    }

    [HttpPost(Name = "CreateSnippetContent")]
    public async Task<ActionResult<SnippetContentDTO>> PostSnippetContent(SnippetContentDTO snippetContentDTO)
    {
        if (snippetContentDTO == null)
        {
            return BadRequest("Snippet content cannot be null.");
        }

        Console.WriteLine($"snippetContentDTO: {snippetContentDTO}");

        var contentId = Nanoid.Generate();

        Console.WriteLine($"ContentId: {contentId}");

        // Simulate saving to a database
        var snippetContent = new SnippetContent
        {
            Content = snippetContentDTO.Content,
            ContentId = contentId,
            IV = snippetContentDTO.IV,
            BurnAfterRead = snippetContentDTO.BurnAfterRead,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = snippetContentDTO.ExpiresAt
        };

        _context.SnippetContent.Add(snippetContent);
        await _context.SaveChangesAsync();

        var createdSnippetContentDTO = new SnippetContentDTO(snippetContent);

        // Return the content ID in the response
        return CreatedAtAction(nameof(GetSnippetContent), new { id = contentId }, contentId);
    }
}