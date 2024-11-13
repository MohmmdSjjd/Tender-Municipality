namespace Application.DTOs.Bid;

public class BidResponse : BaseResponse
{
    public Guid TenderId { get; set; }

    public BidResponse(string message, Guid tenderId):base(message)
    {
        TenderId = tenderId;
    }

}