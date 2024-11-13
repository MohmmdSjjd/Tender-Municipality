using System.Security.Cryptography.X509Certificates;
using Domain.Models.Bid;
using Domain.Models.Bid.DTOs;
using Domain.Models.Tender;
using Domain.Models.Tender.DTO;
using Domain.Models.Tender.Value_Object;
using Domain.Models.User;
using Domain.Models.User.DTOs;
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

    public async Task<Tender?> FindByIdAsync(Guid tenderId)
    {
        return await _context.Tenders.FindAsync(tenderId);
    }

    public async Task<List<FinishedTender>> GetAllTenderAsync()
    {
        return await _context.Tenders
        .AsNoTracking()
        .Where(x => x.TenderDate.EndDate < DateTime.Now)
        .Include(x => x.Bids)
        .ThenInclude(x => x.User)
        .Select(x => new FinishedTender()
        {
            TenderId = x.Id,
            Title = x.Title,
            Description = x.Description,
            Budget = x.Budget,
            LowestBidPrice = x.Bids.Count > 0 ? x.Bids.Min(z => z.Price) : x.Budget.SmallAmount,
            ParticipantCount = x.Bids.Count,
            TenderDate = new TenderDate(x.TenderDate.StartDate, x.TenderDate.EndDate),
            Bids = x.Bids.Count > 0 ? x.Bids.Select(z => new BidDto
            {
                Price = z.Price,
                TenderId = z.TenderId,
                User = new UserDto
                {
                    Id = z.User.Id,
                    UserName = z.User.UserName,
                    Email = z.User.Email
                }
            }).ToList() : null,
            WinnerBid = x.Bids.Count > 0 ? x.Bids.OrderBy(z => z.Price).Select(c => new BidDto
            {
                User = new UserDto()
                {
                    Id = c.User.Id,
                    UserName = c.User.UserName,
                    Email = c.User.Email
                },
                Price = c.Price,
                TenderId = c.TenderId,
            }).FirstOrDefault() : null
        })
        .ToListAsync();
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
            .AsNoTracking()
            .Include(x => x.Bids)
            .ThenInclude(z => z.User)
            .Select(x => new TenderWithDetails
            {
                TenderId = x.Id,
                Title = x.Title,
                Description = x.Description,
                Budget = x.Budget,
                LowestBidPrice = x.Bids.Count > 0 ? x.Bids.Min(z => z.Price) : x.Budget.SmallAmount,
                ParticipantCount = x.Bids.Count,
                TenderDate = new TenderDate(x.TenderDate.StartDate, x.TenderDate.EndDate),
            })
            .ToListAsync();
    }
}