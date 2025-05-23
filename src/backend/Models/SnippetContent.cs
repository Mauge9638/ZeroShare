namespace Backend.Models;


public class SnippetContent
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required string ContentId { get; set; }
    public required string IV { get; set; }
    public bool BurnAfterRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAs { get; set; }
}

public class SnippetContentDTO
{
    public required string Content { get; set; }
    public required string IV { get; set; }
    public bool BurnAfterRead { get; set; } = false;
    public DateTime? ExpiresAs { get; set; } = null;
    public SnippetContentDTO(string content, bool burnAfterRead, DateTime createdAt, DateTime? expiresAs)
    {
        Content = content;
        BurnAfterRead = burnAfterRead;
        ExpiresAs = expiresAs;
    }
    public SnippetContentDTO(SnippetContent snippetContent)
    {
        Content = snippetContent.Content;
        BurnAfterRead = snippetContent.BurnAfterRead;
        ExpiresAs = snippetContent.ExpiresAs;
    }
}