namespace Domain.Models.Bid
{
    public class Bid
    {
        public Guid Id { get; }
        public decimal Price { get; }
        public Guid UserId { get; }
        public Guid TenderId { get; }

        private Bid()
        {
        }
        public Bid(decimal price, Guid userId, Guid tenderId)
        {
            if (price <= 0)
            {
                throw new ArgumentException("قیمت نمی تواند صفر یا کمتر از صفر باشد", nameof(price));

            }
            Id = Guid.NewGuid();
            Price = price;
            UserId = userId;
            TenderId = tenderId;
        }
    }
}
