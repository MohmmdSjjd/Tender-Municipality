using System.ComponentModel.DataAnnotations;
using Domain.Models.User;

namespace Domain.Models.Bid
{
    public class Bid
    {
        public Guid Id { get; }

        [Required(ErrorMessage = "قیمت پیشنهادی نمی‌تواند خالی باشد")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت پیشنهادی باید عددی مثبت باشد")]
        public decimal Price { get; private set; }

        [Required(ErrorMessage = "شناسه کاربر نمی‌تواند خالی باشد")]
        public string UserId { get; private set; }

        public TenderUser User { get; private set; }

        [Required(ErrorMessage = "شناسه مناقصه نمی‌تواند خالی باشد")]
        public Guid TenderId { get; private set; }

        public Tender.Tender Tender { get; private set; }

        private Bid()
        {
        }
        public Bid(decimal price, string userId, Guid tenderId, Tender.Tender tender, TenderUser user)
        {
            if (price <= 0)
            {
                throw new ArgumentException("قیمت نمی تواند صفر یا کمتر از صفر باشد", nameof(price));

            }
            Id = Guid.NewGuid();
            Price = price;
            UserId = userId;
            TenderId = tenderId;
            Tender = tender;
            User = user;
        }

        public void Update(decimal price)
        {
            if (price <= 0)
            {
                throw new ArgumentException("قیمت نمی تواند صفر یا کمتر از صفر باشد", nameof(price));
            }
            Price = price;
        }
    }
}
