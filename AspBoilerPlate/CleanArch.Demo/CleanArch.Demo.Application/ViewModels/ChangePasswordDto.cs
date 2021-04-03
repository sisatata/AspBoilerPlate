using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.ViewModels
{
  public  class ChangePasswordDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
