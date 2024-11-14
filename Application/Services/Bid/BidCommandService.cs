using Application.DTOs.Bid;
using Application.Interfaces.Bid;
using Application.Commands.Bid.CreateBid;

namespace Application.Services.Bid
{
    public class BidCommandService : IBidCommandService
    {
        private readonly ICreateBidCommandHandler _createBidCommandHandler;

        public BidCommandService(ICreateBidCommandHandler createBidCommandHandler)
        {
            _createBidCommandHandler = createBidCommandHandler;
        }

        public async Task<BidResponse> CreateBidAsync(CreateBidCommand command)
        {
            return await _createBidCommandHandler.HandleAsync(command);
        }
    }
}
