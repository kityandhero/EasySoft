using Microsoft.AspNetCore.SignalR;

namespace WebApplicationTest.Hubs;

/// <summary>
/// ChatHub
/// </summary>
public class ChatHub : Hub
{
    /// <summary>
    /// SendMessage
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}