using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebApi.Services;

public class QotdDbService(ILogger<QotdDbService> logger, IDbContextFactory<QotdContext> contextFactory) : IQotdService
{
    public async Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var quotes = await context.Quotes.Include(c => c.Author).ToListAsync();
        var random = new Random();
        var randomQuote = quotes[random.Next(quotes.Count)];

        return new QuoteOfTheDayViewModel
        {
            Id = randomQuote.Id,
            AuthorName = randomQuote.Author?.Name ?? string.Empty,
            AuthorDescription = randomQuote.Author?.Description ?? string.Empty,
            QuoteText = randomQuote.QuoteText,
            AuthorBirthDate = randomQuote.Author?.BirthDate,
            AuthorPhoto = randomQuote.Author?.Photo,
            AuthorPhotoMimeType = randomQuote.Author?.PhotoMimeType
        };
    }
}