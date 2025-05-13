using Application.ViewModels.Qotd;
using Microsoft.AspNetCore.Components;
using Persistence;

namespace UI.Blazor.Components.Pages;
public partial class Home
{
    [Inject] public QotdContext QotdContext { get; set; } = null!;
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }
    private readonly string _color = "text-primary";

    protected override void OnInitialized()
    {
        var authors = QotdContext.Authors.ToList();

        QotdViewModel = new QuoteOfTheDayViewModel
        {
            AuthorName = "Ich",
            AuthorDescription = "Dozent",
            QuoteText = "Larum lierum Löffelstiel",
            AuthorBirthDate = new DateOnly(2000, 07, 13)
        };
    }
}
