namespace Domain.Models.Tender.Value_Object;

public class Budget
{
    public decimal BigAmount { get; }
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
        if (amount < SmallAmount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "مبلغ وارد شده کمتر از حداقل مبلغ مجاز است");
        }

        if (amount > BigAmount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "مبلغ وارد شده بیشتر از حداکثر مبلغ مجاز است");
        }

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "مقدار وارد شده نمی تواند صفر یا منفی باشد");
        }
    }
}
