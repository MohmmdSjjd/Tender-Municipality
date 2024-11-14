using Application.Commands.Tender.CreateTender;
using Application.DTOs.Tender;
using Application.Interfaces.Tender;

namespace Application.Services.Tender
{
    public class TenderCommandService : ITenderCommandService
    {
        private readonly ICreateTenderCommandHandler _createTenderCommandHandler;

        public TenderCommandService(ICreateTenderCommandHandler createTenderCommandHandler)
        {
            _createTenderCommandHandler = createTenderCommandHandler;
        }
        public Task<TenderResponseWithMessage> CreateTenderAsync(CreateTenderCommand request)
        {
            return _createTenderCommandHandler.HandleAsync(request);
        }
    }
}
