using Application.ViewModels.Author;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UI.Blazor.ComponentsLibrary.Components.Author;
public partial class AuthorTable
{
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] public DialogService DialogService { get; set; } = null!;
    [Parameter] public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }
    [Parameter] public EventCallback<Guid> OnAuthorDelete { get; set; }
    private ConfirmDialog? _confirmDialogComponent; //Referenz zur Componente
    private Guid _authorIdToDelete;

    private async Task ShowConfirmDialog(AuthorViewModel authorVm)
    {
        _authorIdToDelete = authorVm.Id;

        //1. Version Klassik
        //if (await JsRuntime.InvokeAsync<bool>("confirm", $"Wollen Sie wirklich den Autor '{authorVm.Name}' löschen?"))
        //{
        //    await OnAuthorDelete.InvokeAsync(authorVm.Id);
        //}

        //2. Version
        //if (await DialogService.ConfirmAsync($"Wollen Sie wirklich den Autor '{authorVm.Name}' löschen?"))
        //{
        //    await OnAuthorDelete.InvokeAsync(authorVm.Id);
        //}

        //3. Version
        _confirmDialogComponent?.Show($"Wollen Sie wirklich den Autor <strong>'{authorVm.Name}'</strong> löschen?");
    }

    private async Task ConfirmDeleteClicked()
    {
        await OnAuthorDelete.InvokeAsync(_authorIdToDelete);
    }
}
