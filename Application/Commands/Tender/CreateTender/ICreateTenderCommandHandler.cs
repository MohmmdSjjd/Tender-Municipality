using Application.DTOs.Tender;

namespace Application.Commands.Tender.CreateTender
{
    public interface ICreateTenderCommandHandler
    {
        Task<TenderResponseWithMessage> HandleAsync(CreateTenderCommand request);
    }
}
