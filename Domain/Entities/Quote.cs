using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Quote
{
    //[Key]
    public Guid Id { get; set; } //QuoteId, ID, Id
    public required string QuoteText { get; set; }
    public Author? Author { get; set; }
    public Guid AuthorId { get; set; }
}