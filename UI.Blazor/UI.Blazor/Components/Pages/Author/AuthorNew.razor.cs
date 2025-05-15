using Application.Contracts.Services;
using Application.Utilities;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace UI.Blazor.Components.Pages.Author;
public partial class AuthorNew
{
    [Inject] public ILogger<AuthorNew> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;
    public AuthorForCreateViewModel? AuthorForCreateVm { get; set; }

    protected override void OnInitialized() => AuthorForCreateVm ??= new() { Name = "", Description = "" };
    
    private async Task HandleValidSubmit(EditContext arg)
    {
        Logger.LogInformation($"AuthorForCreateVm: {AuthorForCreateVm?.LogAsJson()}");
        
        try
        {
            var savedNewAuthorVm = await ServiceManager.AuthorService.AddAuthorAsync(AuthorForCreateVm);

            if (savedNewAuthorVm is not null)
            {
                NavManager.NavigateTo("/authors/overview");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Fehler beim Speichern des Authors: {ex.Message}");
        }
    }

    private void OnInputFileChange(InputFileChangeEventArgs args)
    {
        AuthorForCreateVm!.Photo = args.File;
        //Logger.LogInformation($"Photo: {AuthorForCreateVm!.Photo?.LogAsJson()}");
    }
}
