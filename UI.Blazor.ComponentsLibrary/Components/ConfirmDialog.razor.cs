using Microsoft.AspNetCore.Components;

namespace UI.Blazor.ComponentsLibrary.Components;
public partial class ConfirmDialog
{
    [Parameter, EditorRequired] public string ConfirmTitle { get; set; } = string.Empty;
    [Parameter] public string ConfirmMessage { get; set; } = "Sind Sie sicher?";


}
