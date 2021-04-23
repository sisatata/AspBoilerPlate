using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Threading.Tasks;
namespace CleanArch.Demo.Application.Services
{
   public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailSender(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        public async  Task<bool> SendResetPasswordLink(string userEmail, string link)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailConfiguration.From);
            email.To.Add(MailboxAddress.Parse("aansadiqul@gmail.com"));
            email.Subject = "Password Reset";
            var builder = new BodyBuilder();
           /* if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }*/
            builder.HtmlBody = link;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            return true;
        }
    }
}
