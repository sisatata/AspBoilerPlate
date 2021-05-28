using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArch.Demo.Shared.Constants.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CleanArch.Demo.Application.Settings;
using CleanArch.Demo.Domain.Models;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using CleanArch.Demo.Application.Commands.Model;
using AutoMapper;

namespace CleanArch.Demo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UniversityDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _autoMapper;

        public UserService(UniversityDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, IMapper autoMapper, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _autoMapper = autoMapper;
            _emailSender = emailSender;
        }


        public async Task<string> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,

            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());

                }
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email } is already registered.";
            }
        }
        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            var now = await _context.ApplicationUsers.ToListAsync();
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();


                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                    authenticationModel.RefreshToken = activeRefreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                }
                else
                {
                    var refreshToken = CreateRefreshToken();
                    authenticationModel.RefreshToken = refreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    _context.Update(user);
                    _context.SaveChanges();
                }
                // return claims
                
                authenticationModel.Permissions = await GetClaims(user);
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }
        private async Task<List<string>> GetClaims(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            IList<Claim> claimsOfUser = new List<Claim>();
            List<string> claimListOfUser = new List<string>();
            foreach (var role in userRoles)
            {
                var roleList = await _roleManager.FindByNameAsync(role);
                var claims = await _roleManager.GetClaimsAsync(roleList);
                foreach (var claim in claims)
                {
                    if (!claimsOfUser.Contains(claim))
                    {
                        claimListOfUser.Add(claim.Value.ToString());
                        claimsOfUser.Add(claim);
                    }
                }

            }
            return claimListOfUser;
        }
        public async Task<AuthenticationModel> RefreshTokenAsync(string token)
        {
            var authenticationModel = new AuthenticationModel();
            var user =  _context.ApplicationUsers.SingleOrDefault(x=>x.RefreshTokens.Any(y=>y.Token== token));
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Token did not match any users.";
                return authenticationModel;
            }
            
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Token Not Active.";
                return authenticationModel;
            }
            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.UtcNow;
            //Generate new Refresh Token and save to Database
            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();
            //Generates new jwt
            authenticationModel.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();
            authenticationModel.RefreshToken = newRefreshToken.Token;
           
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
            authenticationModel.Permissions = await GetClaims(user);
            return authenticationModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            ////
            //  var allClaims = await _roleManager.GetClaimsAsync(role);

            ////
            ///
            IList<Claim> claimList = new List<Claim>();

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var roleItem in roles)
            {
                var adminRole = await _roleManager.FindByNameAsync(roleItem);
                var allClaims = await _roleManager.GetClaimsAsync(adminRole);
                foreach (var claimItem in allClaims)
                {
                    claimList.Add(claimItem);
                }

            }

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(roleClaims).Union(claimList);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow
                };

            }
        }

        public async Task DeleteUserAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);

        }

        public async Task<CommonResponseDto> ChangePasswordAsync(ChangePasswordDto model)
        {
            var response = new CommonResponseDto
            {
                Status = false,
                Message = "Can't change password"
            };
            try
            {

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    response.Message = "User not found";
                    return response;
                }
                bool IsPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                if (!IsPasswordCorrect)
                {
                    response.Message = "Password incorrect";
                    return response;
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPassResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (resetPassResult.Succeeded)
                {
                    response.Status = true;
                    response.Message = "Password updated successfully";
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.ToString();
                return response;
            }

        }

        public async Task<bool> CreateRoles(string role)
        {
            try
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<IdentityRole>> GetRoles()
        {
            var roles = _roleManager.Roles;
            return await roles.ToListAsync();
        }

        public async Task<CommonResponseDto> DeleteRole(string role)
        {
            var response = new CommonResponseDto
            {
                Status = false,
                Message = ""

            };
            try
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    response.Status = true;
                    response.Message = "Role does not exist";
                    return response;
                }
                var roleName = await _roleManager.FindByNameAsync(role);
                await _roleManager.DeleteAsync(roleName);
                response.Status = true;
                response.Message = "Role successfully deleted";
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CommonResponseDto> UpdateUser(ApplicationUser user, UserDto model)
        {
            var response = new CommonResponseDto
            {
                Status = false,
                Message = "Can't change password"
            };
            try
            {
                // need to use auto mapper
                user.Name = model.Name;
                await _userManager.UpdateAsync(user);
                response.Status = true;
                response.Message = "User updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommonResponseDto> CreateRole(string role)
        {
            try
            {
                var response = new CommonResponseDto
                {
                    Status = false,
                    Message = "Can't change password"
                };
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    response.Message = "Role successfully created";
                    response.Status = true;
                    return response;
                }
                return response;

            }

            catch (Exception ex)
            {
                throw;
            }


        }
        
        public async Task<bool> AssignRole(string userId, string[] roles)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) throw new ArgumentException("User not found");
                if (user != null)
                    await DeleteUserRoles(user);
                await _userManager.AddToRolesAsync(user, roles);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task DeleteUserRoles(ApplicationUser user)
        {

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
        }
        public async Task<CommonResponseDto> AssignPermissionToRole(string role, string permission)
        {
            var response = new CommonResponseDto
            {
                Status = false,
                Message = "Can't assign role to user"
            };
            try
            {
                var getRole = await _roleManager.FindByNameAsync(role);
                var allPermissions = await _roleManager.GetClaimsAsync(getRole);
                var isPermissionExist = allPermissions.Any(a => a.Type == "Permission" && a.Value == permission);
                if (!isPermissionExist)
                {
                    await _roleManager.AddClaimAsync(getRole, new Claim("Permission", permission));
                    response.Status = true;
                    response.Message = "Permission assign to this role";
                    return response;
                }
                response.Status = false;
                response.Message = "Permission already assigned to this role";
                return response;
            }
            catch( Exception ex)
            {
                throw;
            }
            
        }
        public async Task<ForgotPasswordResponse> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException($"User not found for the email {email}");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var res = await _emailSender.SendResetPasswordLink(email, token);
            var link = token;
            return null;
        }
        public async Task<CommonResponseDto> AddPermissionToRole(string role, string permission)
        {
            var response = new CommonResponseDto
            {
                Status = false,
                Message = "Can't assign role to user"
            };
            try
            {
                var roleName = await _roleManager.FindByNameAsync(role);
                var res = await _roleManager.AddClaimAsync(roleName, new Claim("Permission", permission));
                response.Status = true;
                response.Message = $"{permission} added to {role} successfully";
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
         
        }


    }


}
