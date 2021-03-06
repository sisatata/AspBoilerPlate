using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Domain.Interfaces
{
  public  interface IUserRepository 
    {
        Task<bool> RegisterUser(RegisterVM entity);
    }
}
