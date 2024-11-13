using Domain.Models.Bid.DTOs;

namespace Domain.Models.Tender.DTO
{
    public class FinishedTender : TenderWithDetails
    {
        public List<BidDto> Bids { get; set; }= new();
        public BidDto WinnerBid { get; set; }
    }
}
