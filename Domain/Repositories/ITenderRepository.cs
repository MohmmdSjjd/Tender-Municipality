using Domain.Models.Tender;
using Domain.Models.Tender.DTO;

namespace Domain.Repositories;

public interface ITenderRepository
{
    Task<Tender?> FindByIdAsync(Guid tenderId);
    Task<List<FinishedTender>> GetAllTenderAsync();
    Task<Tender> AddTenderAsync(Tender tender);
    Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync();
}