using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Tender.Value_Object;

public class Budget
{

    [Required(ErrorMessage = "حداکثر مبلغ مجاز نمی‌تواند خالی باشد")]
    [Range(0, double.MaxValue, ErrorMessage = "حداکثر مبلغ مجاز باید عددی مثبت باشد")]
    public decimal BigAmount { get; }

    [Required(ErrorMessage = "حداقل مبلغ مجاز نمی‌تواند خالی باشد")]
    [Range(0, double.MaxValue, ErrorMessage = "حداقل مبلغ مجاز باید عددی مثبت باشد")]
    public decimal SmallAmount { get; }

    public Budget(decimal bigAmount, decimal smallAmount)
    {
        BigAmount = bigAmount;
        SmallAmount = smallAmount;
    }

    // this method is used to check the amount of the budget in bid time
    public bool CheckAmount()
    {
        return BigAmount > 0 && SmallAmount >= 0 && BigAmount > SmallAmount;
    }

    public bool CheckAmount(decimal price)
    {
        return price >= SmallAmount && price <= BigAmount;
    }
}
