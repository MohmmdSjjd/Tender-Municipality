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
        if (bigAmount <= smallAmount)
        {
            throw new ArgumentException("حداقل مبلغ مجاز نمی تواند بیشتر یا برابر با حداکثر مبلغ مجاز باشد");
        }

        if (bigAmount <= 0 || smallAmount <= 0)
        {
            throw new ArgumentException("مقادیر وارد شده نمی تواند صفر یا منفی باشد");
        }

        BigAmount = bigAmount;
        SmallAmount = smallAmount;
    }

    // this method is used to check the amount of the budget in bid time
    public void CheckAmount(decimal amount)
    {
        if (amount < SmallAmount || amount > BigAmount || amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "مبلغ پیشنهاد باید بین حداقل مبلغ مجاز و حداکثر مبلغ مجاز باشد");
        }
    }
}
