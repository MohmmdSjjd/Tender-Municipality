using Application.DTOs.Tender;
using Domain.Models.Tender.DTO;

namespace Application.Queries.Tender.GetAllTenders
{
    public interface IGetAllTendersQueryHandler
    {
        Task<List<TenderResponseWithActiveStatus>> HandleAsync();    
    }           
}
