using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace vKurzuCore.Services
{
    public class EmailSender : IEmailSender , IMyEmailSender
    {
        private const string SenderAdress = "neodpovidat@vkurzu.cz";
        private readonly IConfiguration _configuration;
        private readonly string _emailPassword;
        private readonly string _adminEmails;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailPassword = _configuration.GetSection("AdminData")["EmailPassword"];
            _adminEmails = _configuration.GetSection("AdminData")["AdminEmails"];
        }
        public  async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {

                using (var client = new SmtpClient("smtp.forpsi.com", 587)
                { EnableSsl = true, Credentials = new NetworkCredential(SenderAdress, _emailPassword) })
                {
                    await client.SendMailAsync(new MailMessage()
                    {
                        From = new MailAddress(SenderAdress),
                        Subject = subject,
                        To = { email },
                        Body = htmlMessage,
                        BodyEncoding = Encoding.UTF8,
                        IsBodyHtml = true,

                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public async Task<bool> SendEmailFromForm(string from, string subject, string body)
        {
            if (string.IsNullOrEmpty(from))
            {
                return false;
            }
            try
            {

                var client = new SmtpClient("smtp.forpsi.com", 587) //465
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(SenderAdress, _emailPassword),

                };
                await client.SendMailAsync(SenderAdress, _adminEmails, subject, body);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

    }
}
