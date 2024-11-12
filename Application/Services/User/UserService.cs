using Application.DTOs;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Interfaces.User;
using Domain.Models.User;
using InfraStructure.Security.JWT;
using Microsoft.AspNetCore.Identity;


namespace Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<TenderUser> _userManager;
        private readonly SignInManager<TenderUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<TenderUser> userManager, SignInManager<TenderUser> signInManager, ITokenService tokenService,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new TenderUser { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
               throw new ApiException(string.Join(", ", result.Errors.Select(e => e.Description)),400);
            }

            return new RegisterResponse(user, "ثبت نام با موفقیت انجام شد");
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception(" کاربری با این ایمیل یافت نشد");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                throw new Exception(" رمز عبور اشتباه است");
            }
            
            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _tokenService.GenerateToken(user,userRoles);
            return new AuthResponse(token, DateTime.Now.AddHours(1), user.Id, user.UserName ?? string.Empty, "ورود با موفقیت انجام شد");
        }

        public async Task<AddRoleResponse> AddRoleToUserAsync(AddRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("کاربری با این شناسه یافت نشد");
            }

            var addRoleToUser = await _userManager.AddToRoleAsync(user, request.Role);
            if (!addRoleToUser.Succeeded)
            {
                throw new Exception(string.Join(", ", addRoleToUser.Errors.Select(e => e.Description)));
            }
            return new AddRoleResponse(user.Id, user.UserName ?? string.Empty,request.Role, "نقش جدید با موفقیت به کاربر اضافه شد");
        }
    }
}
