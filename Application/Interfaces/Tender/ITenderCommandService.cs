using Application.DTOs.Tender;
using Domain.Models.Tender.DTO;

namespace Application.Interfaces.Tender
{
    public interface ITenderCommandService
    {
        Task<TenderResponse> CreateTenderAsync(TenderRequest request);
        Task<List<Domain.Models.Tender.Tender>> GetAllTendersAsync();
        Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync();
    }

    

   
}
