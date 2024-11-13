using Application.DTOs.User;

namespace Application.DTOs.Bid;

public class BidResponse : BaseResponse
{
    public Guid TenderId { get; set; }
    public string Message { get; set; }

    public BidResponse(string message, Guid tenderId):base(message)
    {
        TenderId = tenderId;
        Message = message;
    }

}