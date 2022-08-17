using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Core.Utilities.Mail
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(EmailMessage emailMessage)
        {

            EmailConfiguration emailConfiguration = _configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

            var client = new SmtpClient(emailConfiguration.Server, emailConfiguration.Port)
            {
                Credentials = new NetworkCredential(emailConfiguration.Username, emailConfiguration.Password),
                EnableSsl = emailConfiguration.EnableSsl
            };
            return client.SendMailAsync(
                new MailMessage(emailConfiguration.Username, emailMessage.Email, emailMessage.Subject, emailMessage.Body) { IsBodyHtml = true }
                );
        }
    }
}
