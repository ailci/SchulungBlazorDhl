using Application.Contracts.Services;
using Application.ViewModels.Author;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Persistence;

namespace UI.Blazor.Services;

public class AuthorService(QotdContext context) : IAuthorService
{
    public async Task<IEnumerable<AuthorViewModel>> GetAuthorsAsync()
    {
        var authors = await context.Authors.ToListAsync();
        var authorViewModels = authors.Select(a => new AuthorViewModel
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            BirthDate = a.BirthDate,
            Photo = a.Photo,
            PhotoMimeType = a.PhotoMimeType
        });
        return authorViewModels;
    }
}