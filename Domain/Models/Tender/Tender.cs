using Domain.Models.Tender.Value_Object;

namespace Domain.Models.Tender
{
    public class Tender
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public TenderDate TenderDate { get; }
        public Budget Budget { get; }
        public List<Bid.Bid> Bids { get; }
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

        // بررسی فعال بودن مناقصه
        public bool IsActive()
        {
            return TenderDate.IsActive();
        }

        // بررسی منقضی شدن مناقصه
        public bool IsExpired()
        {
            return TenderDate.IsExpired();
        }

        // بررسی بودجه
        public void CheckBudget(decimal amount)
        {
            Budget.CheckAmount(amount);
        }
    }
}
