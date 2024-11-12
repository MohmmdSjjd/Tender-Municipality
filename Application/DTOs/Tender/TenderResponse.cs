using Domain.Models.Tender.Value_Object;

namespace Application.DTOs.Tender
{
    public class TenderResponse : ResponseBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TenderDate TenderDate { get; }
        public Budget Budget { get; }

        public TenderResponse(string title, string description, TenderDate tenderDate, Budget budget, string message) : base(message)
        {
            Title = title;
            Description = description;
            TenderDate = tenderDate;
            Budget = budget;
        }
    }
}
