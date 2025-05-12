using Application.ViewModels.Qotd;

namespace UI.Blazor.Components.Pages;
public partial class Home
{
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }
    private readonly string _color = "text-primary";

    protected override void OnInitialized()
    {
        QotdViewModel = new QuoteOfTheDayViewModel
        {
            AuthorName = "Ich",
            AuthorDescription = "Dozent",
            QuoteText = "Larum lierum Löffelstiel",
            AuthorBirthDate = new DateOnly(2000, 07, 13)
        };
    }
}
