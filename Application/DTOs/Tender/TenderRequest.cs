using Domain.Models.Tender.Value_Object;

namespace Application.DTOs.Tender;

public class TenderRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TenderDate TenderDate { get; }
    public Budget Budget { get; }

    public TenderRequest(string title, string description, TenderDate tenderDate, Budget budget)
    {
        Title = title;
        Description = description;
        TenderDate = tenderDate;
        Budget = budget;
    }
}