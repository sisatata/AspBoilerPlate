using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task DeleteUserAsync(string userId);
        Task<CommonResponseDto> ChangePasswordAsync(ChangePasswordDto model);

        Task<bool> CreateRoles(string role);
        Task<List<IdentityRole>> GetRoles();
        Task<CommonResponseDto> DeleteRole(string role);

        Task<CommonResponseDto> UpdateUser(ApplicationUser user, UserDto model);
        Task<CommonResponseDto> CreateRole(string role);
        Task<bool> AssignRole(string userId, string[] roles);

        Task<CommonResponseDto> AssignPermissionToRole(string role, string permission);
        Task<ForgotPasswordResponse> ForgotPassword(string email);
        Task<CommonResponseDto> AddPermissionToRole(string role, string permission);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
    }
}
