using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace UI.Blazor.ComponentsLibrary;

public class DialogService(IJSRuntime jsRuntime) : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask =
        new(() => jsRuntime
            .InvokeAsync<IJSObjectReference>("import", "./_content/UI.Blazor.ComponentsLibrary/js/dialog.js").AsTask());

    public async ValueTask<bool> ConfirmAsync(string message)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("myConfirm", message);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}