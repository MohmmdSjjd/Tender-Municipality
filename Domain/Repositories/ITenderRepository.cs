using Domain.Models.Tender;
using Domain.Models.Tender.DTO;

namespace Domain.Repositories;

public interface ITenderRepository
{
    Task<List<Tender>> GetAllTenderAsync();
    Task<Tender> AddTenderAsync(Tender tender);
    Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync();
}