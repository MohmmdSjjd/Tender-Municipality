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

        public async Task<List<Bid?>> GetAllAsync()
        {
            return await _context.Bids.ToListAsync();
        }

        public async Task<Bid> AddAsync(Bid bid)
        {
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();

            return bid;
        }
        
    }
}
