using Application.Contracts.Services;
using Application.Utilities;
using Application.ViewModels.Author;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Persistence;

namespace UI.Blazor.Services;

public class AuthorService(ILogger<AuthorService> logger, IDbContextFactory<QotdContext> contextFactory) : IAuthorService
{
    public async Task<IEnumerable<AuthorViewModel>> GetAuthorsAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();

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

    public async Task<bool> DeleteAuthorAsync(Guid authorId)
    {
        logger.LogInformation($"{nameof(DeleteAuthorAsync)} mit authorId: {authorId} aufgerufen...");
        await using var context = await contextFactory.CreateDbContextAsync();

        var authorEntity = await context.Authors.FindAsync(authorId);

        if (authorEntity is null) return false;

        context.Authors.Remove(authorEntity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<AuthorViewModel> AddAuthorAsync(AuthorForCreateViewModel authorForCreateViewModel)
    {
        logger.LogInformation($"{nameof(AddAuthorAsync)} mit AuthorForCreate: {authorForCreateViewModel.LogAsJson()} aufgerufen...");
        await using var context = await contextFactory.CreateDbContextAsync();

        var authorEntity = new Author
        {
            Name = authorForCreateViewModel.Name,
            Description = authorForCreateViewModel.Description,
            BirthDate = authorForCreateViewModel.BirthDate
        };

        if (authorForCreateViewModel.Photo is not null)
        {
            (authorEntity.Photo, authorEntity.PhotoMimeType) = await authorForCreateViewModel.Photo.GetFile();
            //(var fileContent, var fileContentType) = await authorForCreateViewModel.Photo.GetFile();
            //authorEntity.Photo = fileContent;
            //authorEntity.PhotoMimeType = fileContentType;
        }

        await context.Authors.AddAsync(authorEntity);
        await context.SaveChangesAsync();

        return new AuthorViewModel
        {
            Name = authorEntity.Name,
            Description = authorEntity.Description,
            BirthDate = authorEntity.BirthDate,
            PhotoMimeType = authorEntity.PhotoMimeType,
            Photo = authorEntity.Photo
        };
    }
}