using Application.DTOs.Tender;
using Application.Interfaces.Tender;
using Domain.Models.Tender.DTO;
using Domain.Repositories;

namespace Application.Services.Tender;

public class TenderQueryService : ITenderQueryService
{
    private readonly ITenderRepository _tenderRepository;

    public TenderQueryService(ITenderRepository tenderRepository)
    {
        _tenderRepository = tenderRepository;
    }

    public async Task<List<TenderResponseWithActiveStatus>> GetAllTendersAsync()
    {
        var getAllTender = await _tenderRepository.GetAllTenderAsync();
        return getAllTender.Select(t => new TenderResponseWithActiveStatus(t.TenderId, t.Title, t.Description, t.TenderDate, t.Budget, t.TenderDate.IsActive()) { Bids = t.Bids, Winner = t.WinnerBid }).ToList();

    }

    public async Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync()
    {
        var tendersWithDetails = await _tenderRepository.GetInProcessTendersWithDetailsAsync();
        return tendersWithDetails.Where(t => t.TenderDate.IsActive()).ToList();
    }
}