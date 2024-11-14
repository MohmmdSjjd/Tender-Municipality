using Domain.Models.Tender.DTO;

namespace Application.Queries.Tender.GetInProcessTendersWithDetails
{
    public interface IGetInProcessTendersWithDetailsHandler
    {
        Task<List<TenderWithDetails>> HandleAsync();
    }       
}
