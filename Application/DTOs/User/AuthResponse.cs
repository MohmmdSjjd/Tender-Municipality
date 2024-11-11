namespace Application.DTOs.User;

public class AuthResponse:ResponseBase
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }

    public AuthResponse(string token, DateTime expiration, string userId, string userName, string message) : base(message)
    {
        Token = token;
        Expiration = expiration;
        UserId = userId;
        UserName = userName;
    }
}

