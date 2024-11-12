using Application.DTOs.Bid;

namespace Application.Interfaces.Bid
{
    public interface IBidCommandService
    {
        Task<BidResponse> CreateBidAsync(BidRequest request);
        Task<BidResponse> UpdateBidAsync(BidRequest request);
        Task<BidResponse> DeleteBidAsync(int id);
    }
}
