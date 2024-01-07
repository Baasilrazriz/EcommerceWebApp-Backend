namespace EcommerceWebApplication.Service
{
    public interface IWelcomeEmailService
    {
        Task SendWelcomeEmailAsync(string to);
    }
}
