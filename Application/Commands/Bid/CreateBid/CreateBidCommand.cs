namespace Application.Commands.Bid.CreateBid
{
    public class CreateBidCommand
    {
        public string UserId { get; set; }
        public Guid TenderId { get; set; }
        public decimal Price { get; set; }

        public CreateBidCommand(string userId, Guid tenderId, decimal price)
        {
            UserId = userId;
            TenderId = tenderId;
            Price = price;
        }
    }
}
