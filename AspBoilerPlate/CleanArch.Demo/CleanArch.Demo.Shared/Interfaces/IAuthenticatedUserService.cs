using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Shared.Interfaces
{
   public interface IAuthenticatedUserService
    {
        string UserId { get; }
        public string Username { get; }

    }
}
