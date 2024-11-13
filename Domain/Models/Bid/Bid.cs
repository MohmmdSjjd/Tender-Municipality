using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("User")]
        public string UserId { get; private set; }

        public TenderUser User { get; }

        [Required(ErrorMessage = "شناسه مناقصه نمی‌تواند خالی باشد")]
        public Guid TenderId { get; private set; }

        private Bid()
        {
        }
        public Bid(decimal price, string userId, Guid tenderId)
        {
            Id = Guid.NewGuid();
            Price = price;
            UserId = userId;
            TenderId = tenderId;
        }
    }
}
