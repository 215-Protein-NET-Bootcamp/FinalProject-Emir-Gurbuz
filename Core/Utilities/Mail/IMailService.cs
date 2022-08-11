namespace Core.Utilities.Mail
{
    public interface IMailService
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}
