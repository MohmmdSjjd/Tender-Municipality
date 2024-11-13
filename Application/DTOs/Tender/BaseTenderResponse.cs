using Application.DTOs.User;
using Domain.Models.Tender.Value_Object;

namespace Application.DTOs.Tender
{
    public class BaseTenderResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TenderDate TenderDate { get; set; }
        public Budget Budget { get; set; }

        public BaseTenderResponse(string title, string description, TenderDate tenderDate, Budget budget)
        {
            Title = title;
            Description = description;
            TenderDate = tenderDate;
            Budget = budget;
        }
    }
}
