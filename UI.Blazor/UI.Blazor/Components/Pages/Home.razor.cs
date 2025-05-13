using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace UI.Blazor.Components.Pages;

public partial class Home : IDisposable
{
    [Inject] public IServiceManager ServiceManager { get; set; } = null!;
    [Inject] public PersistentComponentState ApplicationState { get; set; } = null!;
    private PersistingComponentStateSubscription _persistingComponentStateSubscription;
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }
    private readonly string _color = "text-primary";

    protected override async Task OnInitializedAsync()
    {
        //3. Lösung Herstellerempfehlung
        _persistingComponentStateSubscription = ApplicationState.RegisterOnPersisting(PersistData);

        if (!ApplicationState.TryTakeFromJson<QuoteOfTheDayViewModel>(nameof(QotdViewModel), out var restoredData))
        {
            //Hol aus DB
            QotdViewModel = await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
        }
        else
        {
            QotdViewModel = restoredData;
        }
    }

    private Task PersistData()
    {
        ApplicationState.PersistAsJson(nameof(QotdViewModel), QotdViewModel);
        return Task.CompletedTask;
    }

    public void Dispose() => _persistingComponentStateSubscription.Dispose();

    //2. Lösung
    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        QotdViewModel = await ServiceManager.QotdService.GetQuoteOfTheDayAsync();
    //        StateHasChanged();
    //    }
    //}
}
