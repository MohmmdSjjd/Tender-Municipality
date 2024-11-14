using Api.Hubs;
using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Api.Services.Notification;

public class NotificationHubService : INotificationHub
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationHubService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BidCreated(string message)
    {
        return _hubContext.Clients.All.SendAsync("BidCreated", message);
    }

    public Task TenderCreated(string message)
    {
        return _hubContext.Clients.All.SendAsync("TenderCreated", message);
    }
}