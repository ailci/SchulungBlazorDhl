using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Qotd;

public class QuoteOfTheDayViewModel
{
    public Guid Id { get; init; }
    public required string QuoteText { get; init; }
    public required string AuthorName { get; init; }
    public required string AuthorDescription { get; init; }
    public DateOnly? AuthorBirthDate { get; init; }
    public byte[]? AuthorPhoto { get; init; }
    public string? AuthorPhotoMimeType { get; init; }
}