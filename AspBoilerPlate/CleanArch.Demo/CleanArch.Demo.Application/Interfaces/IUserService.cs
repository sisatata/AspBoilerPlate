using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.Interfaces
{
 public interface IUserService
    {
        Task RegisterUser(RegisterVM register);
    }
}
