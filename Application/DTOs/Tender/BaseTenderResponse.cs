using Application.DTOs.User;
using Domain.Models.Bid.DTOs;
using Domain.Models.Tender.Value_Object;

namespace Application.DTOs.Tender
{
    public class BaseTenderResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TenderDate TenderDate { get; set; }
        public Budget Budget { get; set; }
        public List<BidDto> Bids { get; set; }

        public BaseTenderResponse(Guid id, string title, string description, TenderDate tenderDate, Budget budget)
        {
            Id = id;
            Title = title;
            Description = description;
            TenderDate = tenderDate;
            Budget = budget;
            Bids = new List<BidDto>();
        }
    }
}
