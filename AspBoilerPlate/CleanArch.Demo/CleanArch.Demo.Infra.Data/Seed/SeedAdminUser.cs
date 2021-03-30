using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Shared.Constants.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

                //Seed Default User
                var defaultUser = new ApplicationUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    //  var userBasic = await userManager.FindByEmailAsync(defaultUser.Email);
                    //  if (userBasic == null)
                    {
                        var now = await userManager.CreateAsync(defaultUser, Authorization.default_password);
                        await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
                    }
                }


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
                    // await roleManager.SeedClaimsForSuperAdmin();
                }



            }
            catch (Exception ex)
            {
                var now = ex;
            }
        }
    }
}
