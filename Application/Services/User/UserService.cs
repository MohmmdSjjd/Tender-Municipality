using Application.DTOs;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Interfaces.User;
using Domain.Models.User;
using InfraStructure.Data;
using InfraStructure.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly TenderContext _context;
        private readonly UserManager<TenderUser> _userManager;
        private readonly SignInManager<TenderUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(TenderContext context,UserManager<TenderUser> userManager, SignInManager<TenderUser> signInManager, ITokenService tokenService,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
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
               throw new ApiException(string.Join(", ", result.Errors.Select(e => e.Description)), StatusCodes.Status400BadRequest);
            }

            return new RegisterResponse(user, "ثبت نام با موفقیت انجام شد");
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
           //var a= _context.Bids.Include(x => x.Tender).Include(x => x.User).ToList();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException(" کاربری با این ایمیل یافت نشد", StatusCodes.Status404NotFound);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                throw new ApiException(" رمز عبور اشتباه است", StatusCodes.Status400BadRequest);
            }
            
            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _tokenService.GenerateToken(user,userRoles);
            return new LoginResponse(token, DateTime.Now.AddHours(1), user.Id, user.UserName ?? string.Empty, "ورود با موفقیت انجام شد");
        }

        public async Task<AddRoleResponse> AddRoleToUserAsync(AddRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new ApiException("کاربری با این شناسه یافت نشد", StatusCodes.Status404NotFound);
            }

            var addRoleToUser = await _userManager.AddToRoleAsync(user, request.Role);
            if (!addRoleToUser.Succeeded)
            {
                throw new ApiException(string.Join(", ", addRoleToUser.Errors.Select(e => e.Description)),StatusCodes.Status400BadRequest);
            }
            return new AddRoleResponse(user.Id, user.UserName ?? string.Empty,request.Role, "نقش جدید با موفقیت به کاربر اضافه شد");
        }
    }
}
