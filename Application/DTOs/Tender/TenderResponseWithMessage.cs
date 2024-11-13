using Domain.Models.Tender.Value_Object;

namespace Application.DTOs.Tender;

public class TenderResponseWithMessage : BaseTenderResponse
{
    public string Message { get; set; }

    public TenderResponseWithMessage(string title, string description, TenderDate tenderDate, Budget budget, string message) : base(title, description, tenderDate, budget)
    {
        Message = message;
    }
}