using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Mail
{
    public class MailManager : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = emailMessage.Subject;

            var messageBody = string.Format(emailMessage.Subject, emailMessage.Content);

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = messageBody
            };
            EmailConfiguration emailConfiguration = _configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(
                   emailConfiguration.Server,
                   emailConfiguration.Port,
                   emailConfiguration.EnableSsl);
                await emailClient.SendAsync(message);
                emailClient.Disconnect(true);
            }
        }
    }
}
