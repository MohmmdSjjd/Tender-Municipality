using Microsoft.AspNet.SignalR;

namespace InfraStructure.Hubs;

public class NotificationHub : Hub
{
    public override async Task OnConnected()
    {
        var user = Context.User;
        if (user != null && user.IsInRole("Admin"))
        {
            await Groups.Add(Context.ConnectionId, "Admin");
        }
        await base.OnConnected();
    }

    public override Task OnDisconnected(bool stopCalled)
    {
        Groups.Remove(Context.ConnectionId, "Admin");
        return base.OnDisconnected(stopCalled);
    }

    public async Task Create(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}