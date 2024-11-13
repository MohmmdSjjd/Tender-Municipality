using Domain.Models.Bid.DTOs;
using Domain.Models.Tender.Value_Object;

namespace Application.DTOs.Tender;

public class TenderResponseWithActiveStatus : BaseTenderResponse
{
    public bool IsActive { get; set; }
    public BidDto Winner { get; set; }

    public TenderResponseWithActiveStatus(Guid id,string title, string description, TenderDate tenderDate, Budget budget, bool isActive) : base(id,title, description, tenderDate, budget)
    {
        IsActive = isActive;
    }
}