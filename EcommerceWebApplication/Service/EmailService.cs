using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Service
{
    public class EmailService : IEmailService
    {
        private const string FromEmail = "baasil86805@gmail.com";
        private const string FromEmailPassword = "jimf vfih dzee cvfn"; // Use App Password if 2FA is enabled


        public async Task SendEmailAsync(string to, string subject, string content)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(FromEmail, FromEmailPassword);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(FromEmail),
                    Subject = subject,
                    Body = content,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(to);

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                  
                    throw new InvalidOperationException("Error sending email.", ex);
                }
                catch (Exception ex)
                {
                  
                    throw new InvalidOperationException("An error occurred.", ex);
                }
            }
        }
    }
}

