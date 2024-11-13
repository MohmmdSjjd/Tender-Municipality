using Domain.Models.Tender.Value_Object;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Tender
{
    public class Tender
    {
        public Guid Id { get; }

        [Required(ErrorMessage = "عنوان مناقصه نمی‌تواند خالی باشد")]
        [MaxLength(100, ErrorMessage = "عنوان مناقصه نمی‌تواند بیشتر از 100 کاراکتر باشد")]
        public string Title { get; }

        [Required(ErrorMessage = "توضیحات مناقصه نمی‌تواند خالی باشد")]
        [MaxLength(500, ErrorMessage = "توضیحات مناقصه نمی‌تواند بیشتر از 500 کاراکتر باشد")]
        public string Description { get; }

        public TenderDate TenderDate { get; }
        public Budget Budget { get; }
        public List<Bid.Bid> Bids { get;}
        private Tender()
        {
        }

        public Tender(string title, string description, TenderDate tenderDate, Budget budget)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            TenderDate = tenderDate;
            Budget = budget;
            Bids = new List<Bid.Bid>();
        }

        public bool CheckBudget(decimal price)
        {
            return Budget.CheckAmount(price);
        }
    }
}
