using Application.Commands.Bid.CreateBid;
using Application.DTOs.Bid;

namespace Application.Interfaces.Bid
{
    public interface IBidCommandService
    {
        Task<BidResponse> CreateBidAsync(CreateBidCommand request);
    }   
}
