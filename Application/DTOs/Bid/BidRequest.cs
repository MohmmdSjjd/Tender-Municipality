using Domain.Models.Tender;
using Domain.Models.User;

namespace Application.DTOs.Bid
{
    public class BidRequest
    {
        public string UserId { get; set; }
        public Guid TenderId { get; set; }
        public decimal Price { get; set; }

        public BidRequest(string userId, Guid tenderId, decimal price)
        {
            UserId = userId;
            TenderId = tenderId;
            Price = price;
        }
    }
}
