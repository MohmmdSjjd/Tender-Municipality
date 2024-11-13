using Domain.Models.Bid;
using Domain.Repositories;
using InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly TenderContext _context;

        public BidRepository(TenderContext context)
        {
            _context = context;
        }

        public async Task<Bid> AddAsync(Bid bid)
        {
            var a = await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();

            return bid;
        }

        public async Task<bool> CheckBidExist(decimal price, Guid tenderId, string userId)
        {
            return await _context.Bids.AnyAsync(b => b.Price == price && b.TenderId == tenderId && b.UserId == userId);
        }
    }
}
