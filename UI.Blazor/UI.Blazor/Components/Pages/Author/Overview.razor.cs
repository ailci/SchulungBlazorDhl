using Application.Contracts.Services;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;

namespace UI.Blazor.Components.Pages.Author;
public partial class Overview
{
    [Inject] public ILogger<Overview> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    public IEnumerable<AuthorViewModel>? AuthorsVm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetAuthors();
    }

    public async Task GetAuthors()
    {
        AuthorsVm = (await ServiceManager.AuthorService.GetAuthorsAsync()).OrderBy(c => c.Name);
    }

    private async Task DeleteAuthor(Guid authorId)
    {
        Logger.LogInformation($"Autor löschen aufgerufen mit Id: {authorId}");

        var isDeleted = await ServiceManager.AuthorService.DeleteAuthorAsync(authorId);

        if (isDeleted)
        {
            await GetAuthors();
        }
    }
}
