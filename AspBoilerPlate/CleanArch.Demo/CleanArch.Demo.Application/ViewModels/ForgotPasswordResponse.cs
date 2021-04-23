using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.ViewModels
{
   public class ForgotPasswordResponse
    {
        public string Email { get; set; }
        public string PasswordResetToken { get; set; }
    }
}
