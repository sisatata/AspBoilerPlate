using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.ViewModels
{
    public class EmailConfiguration
    {
        public string From { get; set; } = "hudybany@gmail.com";
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public string UserName { get; set; } = "hudybany@gmail.com";
        public string Password { get; set; } = "12345gmail//";
    }
}
