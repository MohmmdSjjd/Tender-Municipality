using Application.DTOs;
using Application.DTOs.User;
using Domain.Models.User;

namespace Application.Interfaces.User;

public interface IUserService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AddRoleResponse> AddRoleToUserAsync(AddRoleRequest request);
}