using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Bid
{
    public class Bid
    {
        public Guid Id { get; }

        [Required(ErrorMessage = "قیمت پیشنهادی نمی‌تواند خالی باشد")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت پیشنهادی باید عددی مثبت باشد")]
        public decimal Price { get; private set; }

        [Required(ErrorMessage = "شناسه کاربر نمی‌تواند خالی باشد")]
        public Guid UserId { get; private set; }

        [Required(ErrorMessage = "شناسه مناقصه نمی‌تواند خالی باشد")]
        public Guid TenderId { get; private set; }

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
