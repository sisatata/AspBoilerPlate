using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Infra.Data.Repository.User
{
  public  class UserRepository : IUserRepository
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UniversityDBContext universityDBContext, UserManager<ApplicationUser> userManager)
        {
          
            _userManager = userManager;

        }

        public async Task<bool> RegisterUser(Domain.Models.RegisterVM entity)
        {
            try
            {
                var user = new ApplicationUser { Email = entity.Email, UserName = entity.Name };
                var result = await _userManager.CreateAsync(user, entity.Password);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
            

        }
    }
}
