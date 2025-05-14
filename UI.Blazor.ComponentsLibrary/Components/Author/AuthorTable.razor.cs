using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;

namespace UI.Blazor.ComponentsLibrary.Components.Author;
public partial class AuthorTable
{
    [Parameter]
    public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }

    private Task ShowConfirmDialog(AuthorViewModel author)
    {
        return Task.CompletedTask;
    }
}
