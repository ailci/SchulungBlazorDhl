using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UI.Blazor.ComponentsLibrary.Components.Author;
public partial class AuthorTable
{
    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;
    [Parameter] public IEnumerable<AuthorViewModel>? AuthorViewModels { get; set; }

    private async Task ShowConfirmDialog(AuthorViewModel authorVm)
    {
        //1. Version Klassik
        if (await JsRuntime.InvokeAsync<bool>("confirm", $"Wollen Sie wirklich den Autor '{authorVm.Name}' l�schen?"))
        {
            //TODO: Eltern benachrichtigen zum L�schen
        }
    }
}
