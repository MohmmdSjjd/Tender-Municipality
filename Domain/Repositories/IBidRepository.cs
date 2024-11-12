using Domain.Models.Bid;

namespace Domain.Repositories
{
    public interface IBidRepository
    {
        Task<List<Bid?>> GetAllAsync();
        Task<Bid> AddAsync(Bid bid);
    }
}
