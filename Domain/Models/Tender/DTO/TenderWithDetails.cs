using Domain.Models.Tender.Value_Object;

namespace Domain.Models.Tender.DTO
{
    public class TenderWithDetails
    {
        public Guid TenderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Budget Budget { get; set; }
        public int? ParticipantCount { get; set; }
        public decimal? LowestBidPrice { get; set; }
        public TenderDate TenderDate { get; set; }
    }
}
