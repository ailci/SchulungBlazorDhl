using Microsoft.AspNetCore.Components;

namespace UI.Blazor.ComponentsLibrary.Components;
public partial class ConfirmDialog
{
    [Parameter, EditorRequired] public string ConfirmTitle { get; set; } = string.Empty;
    [Parameter] public string ConfirmMessage { get; set; } = "Sind Sie sicher?";
    [Parameter] public EventCallback<bool> OnConfirmClicked { get; set; }
    private bool _showConfirm;
    private MarkupString _convertedConfirmMessage;

    protected override void OnInitialized()
    {
        _convertedConfirmMessage = new MarkupString(ConfirmMessage);
    }

    public void Show()
    {
        _showConfirm = true;
        StateHasChanged();
    }

    public void Show(string message)
    {
        _showConfirm = true;
        ConfirmMessage = message;
        _convertedConfirmMessage = new MarkupString(Markdig.Markdown.ToHtml(ConfirmMessage));
        StateHasChanged();
    }

    private async Task OnConfirmChange(bool deleteConfirmed)
    {
        _showConfirm = false;

        if (deleteConfirmed)
        {
            await OnConfirmClicked.InvokeAsync(deleteConfirmed);
        }
    }
}
