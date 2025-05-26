using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers;


[ApiController]
[Route("[controller]")]
public class SnippetController(ILogger<SnippetController> logger, ISnippetService snippetService) : ControllerBase
{
    private readonly ILogger<SnippetController> _logger = logger;
    private readonly ISnippetService _snippetService = snippetService;

    [HttpGet("{contentId}", Name = "GetSnippet")]
    public async Task<ActionResult<SnippetDTO>> GetSnippet(string contentId)
    {
        try
        {
            SnippetDTO? snippetDTO = await _snippetService.GetSnippetAsync(contentId);
            if (snippetDTO == null)
            {
                return NotFound($"Snippet with ContentId '{contentId}' not found.");
            }

            return Ok(snippetDTO);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Snippet with ContentId '{contentId}' not found.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving snippet with ContentId '{ContentId}'", contentId);
            return StatusCode(500, "An error occurred while retrieving the snippet.");
        }
    }

    [HttpPost(Name = "CreateSnippet")]
    public async Task<ActionResult<SnippetDTO>> PostSnippet(SnippetDTO snippetDTO)
    {
        if (snippetDTO == null)
        {
            return BadRequest("Snippet content cannot be null.");
        }

        try
        {
            var (contentId, createdSnippetDTO) = await _snippetService.CreateSnippetAsync(snippetDTO);

            return CreatedAtAction(nameof(GetSnippet), new { contentId }, contentId);
        }
        catch
        {
            _logger.LogError("An error occurred while creating the snippet.");
            return StatusCode(500, "Internal server error while creating the snippet.");
        }
    }
}