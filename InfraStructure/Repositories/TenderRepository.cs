using Domain.Models.Tender;
using Domain.Models.Tender.DTO;
using Domain.Models.Tender.Value_Object;
using Domain.Repositories;
using InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repositories;

public class TenderRepository : ITenderRepository
{
    private readonly TenderContext _context;

    public TenderRepository(TenderContext context)
    {
        _context = context;
    }

    public async Task<List<Tender>> GetAllTenderAsync()
    {
        return await _context.Tenders
            .Include(x => x.Bids)
            .ThenInclude(x => x.User).ToListAsync();
    }

    public async Task<Tender> AddTenderAsync(Tender tender)
    {
        await _context.Tenders.AddAsync(tender);
        await _context.SaveChangesAsync();

        return tender;
    }

    public async Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync()
    {
        return await _context.Tenders
            .Include(x => x.Bids)
            .Select(x => new TenderWithDetails
            {
                TenderId = x.Id,
                Title = x.Title,
                LowestBidPrice = x.Bids.Count > 0 ? x.Bids.Min(z => z.Price) : x.Budget.SmallAmount,
                ParticipantCount = x.Bids.Count,
                TenderDate = new TenderDate(x.TenderDate.StartDate, x.TenderDate.EndDate)
            })
            .ToListAsync();
    }
}