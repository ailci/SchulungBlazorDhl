using Application;
using Application.Contracts.Services;
using Application.ViewModels.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace UI.Blazor.Components.Pages.Author;
public partial class Overview : IAsyncDisposable
{
    [Inject] public ILogger<Overview> Logger { get; set; } = null!;
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;
    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [CascadingParameter] public Task<AuthenticationState>? AuthenticationState { get; set; }
    [Inject] public IAuthorizationService AuthorizationService { get; set; } = null!;
    public IEnumerable<AuthorViewModel>? AuthorsVm { get; set; }
    private HubConnection? _hubConnection;

    protected override async Task OnInitializedAsync()
    {
        //1. Variante
        //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        ////2. Variante
        //var authState = await AuthenticationState;

        //var user = authState?.User;

        //if (user?.Identity is not null && user.Identity.IsAuthenticated)
        //{
        //    Logger.LogInformation($"Benutzer {user.Identity.Name} ist authentifiziert");
        //    //SignalR
        //}

        //3. Variante
        //var authState = await AuthenticationState;
        //var authorizationResult =
        //    await AuthorizationService.AuthorizeAsync(authState.User, PolicyConstants.IsAtLeastProfessionEmployee);

        //if (authorizationResult.Succeeded)
        //{
        //    Logger.LogInformation($"Benutzer {authState.User.Identity?.Name} ist berechtigt");
            await ActivateSignalR();

            await GetAuthors();
        //}
    }

    public async Task GetAuthors()
    {
        AuthorsVm = (await ServiceManager.AuthorService.GetAuthorsAsync()).OrderBy(c => c.Name);
    }

    private async Task DeleteAuthor(Guid authorId)
    {
        Logger.LogInformation($"Autor löschen aufgerufen mit Id: {authorId}");

        var authState = await AuthenticationState;
        var authorizationResult = await AuthorizationService.AuthorizeAsync(authState.User, authorId, PolicyConstants.NoOneIsAllowedToDeleteDefaultAuthors);

        if (authorizationResult.Succeeded)
        {
            var isDeleted = await ServiceManager.AuthorService.DeleteAuthorAsync(authorId);

            if (isDeleted)
            {
                if (_hubConnection is not null && _hubConnection.State == HubConnectionState.Connected)
                {
                    await _hubConnection.SendAsync("UpdatedAuthors", $"Autor mit Id {authorId} gelöscht...");
                }
                //await GetAuthors();
            }
        }
        else
        {
            Logger.LogError("Benutzer ist nicht berechtigt den Autor zu löschen");
        }
    }

    private async Task ActivateSignalR()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(NavManager.ToAbsoluteUri("/authorLocalHub"))
            .Build();

        _hubConnection.On<string>("AuthorsUpdate", async message =>
        {
            Logger.LogInformation($"SignalR Nachricht empfangen: {message}");

            await GetAuthors();

            await InvokeAsync(StateHasChanged);
        });

        await _hubConnection.StartAsync();

        if (_hubConnection is not null && _hubConnection.State == HubConnectionState.Connected)
        {
            Logger.LogInformation("SignalR connected...");
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }

    private void NavigateToAuthorNew()
    {
        NavManager.NavigateTo("/authors/new");
    }
}
