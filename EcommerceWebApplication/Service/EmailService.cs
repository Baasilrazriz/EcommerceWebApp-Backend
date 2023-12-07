namespace EcommerceWebApplication.Service
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string content)
        {
            // Implement the method to send emails
            // You might use SMTP client or a third-party service like SendGrid, etc.
        }
    }
}
