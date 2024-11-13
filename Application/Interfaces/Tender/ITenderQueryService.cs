using Application.DTOs.Tender;
using Domain.Models.Tender.DTO;

namespace Application.Interfaces.Tender
{
    public interface ITenderQueryService
    {
        Task<List<TenderResponseWithActiveStatus>> GetAllTendersAsync();
        Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync();
    }
}
