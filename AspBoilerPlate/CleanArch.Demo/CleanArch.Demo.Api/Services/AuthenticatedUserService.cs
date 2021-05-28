using CleanArch.Demo.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Demo.Api.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        public AuthenticatedUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool? IsAuthenticated => _httpContext.HttpContext.User?.Identity.IsAuthenticated;

     public string UserId => _httpContext.HttpContext?.User?.Claims
                                .Where(c => c.Type == "uid")
                                .Select(x => x.Value).FirstOrDefault();

        string IAuthenticatedUserService.Username => throw new NotImplementedException();
    }
}
