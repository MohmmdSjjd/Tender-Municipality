namespace Application.DTOs.User;

public class AddRoleResponse:ResponseBase
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }

    public AddRoleResponse(string userId, string userName, string role, string message) : base(message)
    {
        UserId = userId;
        Role = role;
        UserName = userName;
    }
}
