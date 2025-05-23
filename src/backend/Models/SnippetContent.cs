namespace Backend.Models;

public class SnippetContent
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required string ContentId { get; set; }
    public required string IV { get; set; }
    public bool BurnAfterRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; } = null;

    public SnippetContent() { }
    public SnippetContent(
        string content,
        string contentId,
        string iv,
        bool burnAfterRead,
        DateTime createdAt,
        DateTime expiresAt)
    {
        Content = content;
        ContentId = contentId;
        IV = iv;
        BurnAfterRead = burnAfterRead;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

}

public class SnippetContentDTO
{
    public required string Content { get; set; }
    public required string IV { get; set; }
    public bool BurnAfterRead { get; set; } = false;
    public DateTime? ExpiresAt { get; set; } = null;

    public SnippetContentDTO() { }
    public SnippetContentDTO(string content, string iv, bool burnAfterRead, DateTime? expiresAt)
    {
        Content = content;
        IV = iv;
        BurnAfterRead = burnAfterRead;
        ExpiresAt = expiresAt;
    }

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public SnippetContentDTO(SnippetContent snippetContent)
    {
        Content = snippetContent.Content;
        IV = snippetContent.IV;
        BurnAfterRead = snippetContent.BurnAfterRead;
        ExpiresAt = snippetContent.ExpiresAt;
    }
}