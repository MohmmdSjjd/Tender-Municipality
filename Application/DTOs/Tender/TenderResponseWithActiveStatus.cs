using Domain.Models.Tender.Value_Object;

namespace Application.DTOs.Tender;

public class TenderResponseWithActiveStatus : BaseTenderResponse
{
    public bool IsActive { get; set; }

    public TenderResponseWithActiveStatus(string title, string description, TenderDate tenderDate, Budget budget, bool isActive) : base(title, description, tenderDate, budget)
    {
        IsActive = isActive;
    }
}