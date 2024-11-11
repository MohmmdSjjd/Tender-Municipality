using Application.DTOs;
using Application.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
   {
       [ApiController]
       [Route("api/[controller]")]
       public class UserController : ControllerBase
       {
           private readonly IUserService _userService;

           public UserController(IUserService userService)
           {
               _userService = userService;
           }

           [HttpPost("register")]
           public async Task<IActionResult> Register(RegisterRequest request)
           {
               var response = await _userService.RegisterAsync(request);
               return Ok(response);
           }

           [HttpPost("login")]
           public async Task<IActionResult> Login(LoginRequest request)
           {
               var response = await _userService.LoginAsync(request);
               return Ok(response);
           }
       }
   }
   