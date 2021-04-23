using CleanArch.Demo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.Interfaces
{
  public interface IEmailSender
    {
//Task SendMail(EmailModel emailModel, EmailConfiguration settings);
        Task<bool> SendResetPasswordLink(string userEmail, string link);
    }
}
