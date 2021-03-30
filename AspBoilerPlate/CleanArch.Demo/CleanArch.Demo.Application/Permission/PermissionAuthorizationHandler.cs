using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }
            var permissionss = context.User.Claims.Where(x => x.Type == "Permission" &&
                                                            x.Value == requirement.Permission &&
                                                            x.Issuer == "LOCAL AUTHORITY");

            var types = context.User.Claims.Select(x => x.Type == ClaimTypes.Role);
            if (permissionss.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
