using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Tender.Value_Object;

public class TenderDate
{
    [Required(ErrorMessage = "تاریخ شروع نمی‌تواند خالی باشد")]
    public DateTime StartDate { get; }

    [Required(ErrorMessage = "تاریخ پایان نمی‌تواند خالی باشد")]
    public DateTime EndDate { get; }

    public TenderDate(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
    // check if the tender is active
    public bool IsActive()
    {
        var now = DateTime.Now;
        return now >= StartDate && now <= EndDate;
    }

    // check tender date is valid
    public bool IsValid()
    {
        return StartDate < EndDate;
    }

}