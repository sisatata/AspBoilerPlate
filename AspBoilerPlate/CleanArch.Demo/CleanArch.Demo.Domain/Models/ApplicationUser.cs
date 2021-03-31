using CleanArch.Demo.Domain.Models;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArch.Demo.Infra.Data.Context
{
    public class ApplicationUser : IdentityUser
    {


        public List<RefreshToken> RefreshTokens { get; set; }


    }
}
