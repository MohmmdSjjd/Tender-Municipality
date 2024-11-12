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
        if (startDate >= endDate) 
            throw new Exception("زمان شروع باید قبل از زمان پایان باشد");
        
        StartDate = startDate;
        EndDate = endDate;
    }
    // check if the tender is active
    public bool IsActive()
    {
        var now = DateTime.Now;
        return now >= StartDate && now <= EndDate;
    }

    // check if the tender is expired
    public bool IsExpired()
    {
        var now = DateTime.Now;
        return now > EndDate;
    }
}