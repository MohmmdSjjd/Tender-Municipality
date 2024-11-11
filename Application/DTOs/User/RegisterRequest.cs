namespace Application.DTOs;

public class RegisterRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public RegisterRequest(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
}