using Domain.Models.User;

namespace Application.DTOs.User;

public class RegisterResponse : BaseResponse
{
    public TenderUser User { get; set; }

    public RegisterResponse(TenderUser user, string message) : base(message)
    {
        User = user;
    }
}