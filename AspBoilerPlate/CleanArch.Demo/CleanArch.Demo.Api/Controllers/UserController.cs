using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Infra.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanArch.Demo.Api.Controllers
{
    [Route("Api/[controller]")]

    [ApiController]
    public class UserController : ControllerBase
    {
       
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
       
        public string LoggedInUser => User.Identity.Name;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto request)
        {
            var result = await _userService.RegisterAsync(request);
            return Ok(result);
        }
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
             SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            
            await _userService.DeleteUserAsync(userId);
          
            return Ok(true);
        }
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);
            return Ok(user);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordDto changePasswordDto)
        {
           var result =  await _userService.ChangePasswordAsync(changePasswordDto);
            return Ok(result);
        }
        [HttpPost("create-role")]
       public async Task<IActionResult> CreateRole(string role)
        {
            var res = await _userService.CreateRoles(role);
            return Ok(true);
        }
        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var res = await _userService.GetRoles();
            return Ok(res);
        }
        [HttpPost("delete-role")]

        public async Task<IActionResult> DeleteRole(string role)
        {
            var res = await _userService.DeleteRole(role);
            return Ok(res);
        }


    }
}
