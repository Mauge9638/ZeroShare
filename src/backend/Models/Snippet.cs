namespace Backend.Models;

public class Snippet
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required string ContentId { get; set; }
    public required string IV { get; set; }
    public bool BurnAfterRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; } = null;

    public DateTime LastAccessedAt { get; set; }

    public Snippet() { }
    public Snippet(
        string content,
        string contentId,
        string iv,
        bool burnAfterRead,
        DateTime createdAt,
        DateTime expiresAt,
        DateTime lastAccessedAt
        )
    {
        Content = content;
        ContentId = contentId;
        IV = iv;
        BurnAfterRead = burnAfterRead;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
        LastAccessedAt = lastAccessedAt;
    }

}

public class SnippetDTO
{
    public required string Content { get; set; }
    public required string IV { get; set; }
    public bool BurnAfterRead { get; set; } = false;
    public DateTime? ExpiresAt { get; set; } = null;

    public DateTime LastAccessedAt { get; set; }

    public SnippetDTO() { }
    public SnippetDTO(string content, string iv, bool burnAfterRead, DateTime? expiresAt, DateTime lastAccessedAt)
    {
        Content = content;
        IV = iv;
        BurnAfterRead = burnAfterRead;
        ExpiresAt = expiresAt;
        LastAccessedAt = lastAccessedAt;
    }

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public SnippetDTO(Snippet snippet)
    {
        Content = snippet.Content;
        IV = snippet.IV;
        BurnAfterRead = snippet.BurnAfterRead;
        ExpiresAt = snippet.ExpiresAt;
        LastAccessedAt = snippet.LastAccessedAt;
    }
}

public class SnippetViewDTO
{
    public required string Content { get; set; }
    public required string IV { get; set; }

    public SnippetViewDTO() { }
    public SnippetViewDTO(string content, string iv)
    {
        Content = content;
        IV = iv;
    }

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public SnippetViewDTO(Snippet snippet)
    {
        Content = snippet.Content;
        IV = snippet.IV;
    }
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public SnippetViewDTO(SnippetDTO snippetDTO)
    {
        Content = snippetDTO.Content;
        IV = snippetDTO.IV;
    }
}