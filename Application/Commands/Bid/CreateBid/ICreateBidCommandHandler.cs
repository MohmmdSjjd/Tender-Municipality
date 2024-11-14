using Application.DTOs.Bid;

namespace Application.Commands.Bid.CreateBid
{
    public interface ICreateBidCommandHandler
    {
        Task<BidResponse> HandleAsync(CreateBidCommand request);
    }
}
