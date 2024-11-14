namespace Application.Interfaces
{
    public interface INotificationHub
    {
        Task BidCreated(string message);
        Task TenderCreated(string message);
    }
}
