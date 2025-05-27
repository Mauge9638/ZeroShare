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

    public Snippet() { }
    public Snippet(
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

public class SnippetDTO
{
    public required string Content { get; set; }
    public required string IV { get; set; }
    public bool BurnAfterRead { get; set; } = false;
    public DateTime? ExpiresAt { get; set; } = null;

    public SnippetDTO() { }
    public SnippetDTO(string content, string iv, bool burnAfterRead, DateTime? expiresAt)
    {
        Content = content;
        IV = iv;
        BurnAfterRead = burnAfterRead;
        ExpiresAt = expiresAt;
    }

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public SnippetDTO(Snippet snippet)
    {
        Content = snippet.Content;
        IV = snippet.IV;
        BurnAfterRead = snippet.BurnAfterRead;
        ExpiresAt = snippet.ExpiresAt;
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