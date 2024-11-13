using Domain.Models.Bid;

namespace Domain.Repositories
{
    public interface IBidRepository
    {
        Task<Bid> AddAsync(Bid bid);
        Task<bool> CheckBidExist(decimal price, Guid tenderId, string userId);
    }
}
