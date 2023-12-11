namespace EcommerceWebApplication.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string content);
    }
}
