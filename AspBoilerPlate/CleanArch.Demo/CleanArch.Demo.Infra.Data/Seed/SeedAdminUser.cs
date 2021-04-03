using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Shared.Constants.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Infra.Data.Seed
{
  public static  class SeedAdminUser
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                //Seed Roles
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

               

                var adminUser = new ApplicationUser
                {
                    UserName = "superadmin",
                    Email = "superadmin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                if (userManager.Users.All(u => u.Id != adminUser.Id))
                {
                    var user = await userManager.FindByEmailAsync(adminUser.Email);
                    if (user == null)
                    {

                        await userManager.CreateAsync(adminUser, "123Pa$$word!");
                        await userManager.AddToRoleAsync(adminUser, Authorization.default_role.ToString());
                        await userManager.AddToRoleAsync(adminUser, Authorization.default_Admin.ToString());
                        await userManager.AddToRoleAsync(adminUser, Authorization.default_Mode.ToString());

                    }
                     await roleManager.SeedClaimsForSuperAdmin();
                }



            }
            catch (Exception ex)
            {
                var now = ex;
            }
        }
        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Administrator");
            await roleManager.AddPermissionClaim(adminRole, "Products");
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            try
            {
                var allClaims = await roleManager.GetClaimsAsync(role);
                var allPermissions = Permissions.GeneratePermissionsForModule(module);
                foreach (var permission in allPermissions)
                {
                    if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                    {
                        await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
