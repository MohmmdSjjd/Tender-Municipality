using Application.Commands.Tender.CreateTender;
using Application.DTOs.Tender;

namespace Application.Interfaces.Tender
{
    public interface ITenderCommandService
    {
        Task<TenderResponseWithMessage> CreateTenderAsync(CreateTenderCommand request);
    }   
}
