using Application.DTOs.Tender;
using Application.Interfaces.Tender;
using Application.Queries.Tender.GetAllTenders;
using Application.Queries.Tender.GetInProcessTendersWithDetails;
using Domain.Models.Tender.DTO;
using Domain.Repositories;

namespace Application.Services.Tender;

public class TenderQueryService : ITenderQueryService
{
    private readonly ITenderRepository _tenderRepository;
    private readonly IGetAllTendersQueryHandler _getAllTendersQueryHandler;
    private readonly IGetInProcessTendersWithDetailsHandler _getInProcessTendersWithDetailsHandler;

    public TenderQueryService(ITenderRepository tenderRepository, IGetAllTendersQueryHandler getAllTendersQueryHandler, IGetInProcessTendersWithDetailsHandler getInProcessTendersWithDetailsHandler)
    {
        _tenderRepository = tenderRepository;
        _getAllTendersQueryHandler = getAllTendersQueryHandler;
        _getInProcessTendersWithDetailsHandler = getInProcessTendersWithDetailsHandler;
    }

    public async Task<List<TenderResponseWithActiveStatus>> GetAllTendersAsync()
    {
        return await _getAllTendersQueryHandler.HandleAsync();

    }

    public async Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync()
    {
        return await _getInProcessTendersWithDetailsHandler.HandleAsync();
    }
}