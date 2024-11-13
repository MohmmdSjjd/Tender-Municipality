using Domain.Models.Tender.Value_Object;

namespace Application.DTOs.Tender;

public class TenderResponseWithMessage : BaseTenderResponse
{
    public string Message { get; set; }

    public TenderResponseWithMessage(Guid id,string title, string description, TenderDate tenderDate, Budget budget, string message) : base(id,title, description, tenderDate, budget)
    {
        Message = message;
    }
}