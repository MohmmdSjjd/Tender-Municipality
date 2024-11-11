namespace Application.DTOs.User
{
    public class AddRoleRequest
    {
        public string UserId { get; set; }
        public string Role { get; set; }

        public AddRoleRequest(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }
    }
}
