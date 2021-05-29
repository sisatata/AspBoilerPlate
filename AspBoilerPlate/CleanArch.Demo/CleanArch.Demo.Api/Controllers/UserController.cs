using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<EmailConfiguration> _mailServerSettings;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IOptions<EmailConfiguration> mailServerSettings)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _mailServerSettings = mailServerSettings;

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
        [Authorize(Roles = "Administrator")]
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
            var result = await _userService.ChangePasswordAsync(changePasswordDto);
            return Ok(result);
        }
      
        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var res = await _userService.GetRoles();
            return Ok(res);
        }
        [HttpPost("delete-role")]
        //   [Authorize(Policy = PolicyTypes.Users.EditRole)]

        public async Task<IActionResult> DeleteRole(string role)
        {
            var res = await _userService.DeleteRole(role);
            return Ok(res);
        }
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserDto model)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);
            var res = await _userService.UpdateUser(user, model);

            return Ok(res);

        }
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(string role)
        {
            var res = await _userService.CreateRole(role);
            return Ok(res);
        }
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserRoleDTO model)
        {
            var res = await _userService.AssignRole(model.UserId, model.Roles);
            return Ok(res);
        }
        [HttpPost("assign-permission")]
        public async Task<IActionResult> AssignPermissionToRole(string role, string permission)
        {
            var res = await _userService.AssignPermissionToRole(role, permission);
            return Ok(res);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut(string role, string permission)
        {
            await _signInManager.SignOutAsync();
            return Ok(true);
            
        }
        [HttpGet("isAuthenticated")]
        public async Task<IActionResult> IsAuthenticated()
        {
            var now =  _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            return Ok(true);

        }
        [HttpPost("forgot-password")]
       
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {

            var res = await _userService.ForgotPassword(forgotPasswordModel.Email);
          //  EmailHelper emailHelper = new EmailHelper();
           // bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            return Ok();
            

        }
        [HttpPost("assign-permission-role")]
        public async Task<IActionResult> AddPermissionToRole(string role , string permission)
        {
            var res = await _userService.AddPermissionToRole(role, permission);
            return Ok(res);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> GetRefreshTokenAsync(string token)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userService.RefreshTokenAsync(refreshToken);
            return Ok(response);
        }

    }
}
