namespace Application.ViewModels.Author;

public class AuthorViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly? BirthDate { get; set; }
    public byte[]? Photo { get; set; }
    public string? PhotoMimeType { get; set; }
}