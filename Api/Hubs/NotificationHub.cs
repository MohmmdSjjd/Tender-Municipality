using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var user = Context.User;
        if (user != null && user.IsInRole("Admin"))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = Context.User;
        if (user != null && user.IsInRole("Admin"))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admin");
        }
        await base.OnDisconnectedAsync(exception);
    }
}