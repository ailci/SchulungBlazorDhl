using Microsoft.AspNetCore.SignalR;

namespace UI.Blazor.Hub;

public class AuthorLocalHub(ILogger<AuthorLocalHub> logger) 
    : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task UpdatedAuthors(string message)
    {
        logger.LogInformation($"SignalR Nachricht gesendet (AuthorLocalHub): {message}");

        //Send the message to all connected client
        await Clients.All.SendAsync("AuthorsUpdate", message);
    }
}