using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Service
{
    public class WelcomeEmailService : IWelcomeEmailService
    {
        private const string FromEmail = "baasil86805@gmail.com";
        private const string FromEmailPassword = "jimf vfih dzee cvfn"; 
        private const string WelcomeSubject = "Welcome to DoorDash";

        public async Task SendWelcomeEmailAsync(string to)
        {
            var emailBody = GetEmailBody(to);

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(FromEmail, FromEmailPassword);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(FromEmail),
                    Subject = WelcomeSubject,
                    Body = emailBody,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                    throw new InvalidOperationException("Error sending welcome email.", ex);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("An error occurred while sending welcome email.", ex);
                }
            }
        }
        private string GetEmailBody(string email)
        {
            
            var htmlTemplate = @"
        <html>
            <body>
                <p>Hello {0},</p>
                <p>Welcome to DoorDash!</p>
<p>It's great to have you with us again. As you navigate through our diverse range of products, we hope you find everything you're looking for - and more. Remember, we're here to make your shopping experience as seamless and enjoyable as possible.  </p>
                <p>If you did not register for a DoorDash account, please ignore this email.</p>
                   <p>If you have any questions, please don't hesitate to contact us at <a href='mailto:doordash@gmail.com'>doordash@gmail/.com</a></p>

            </body>
        </html>";

          
            return string.Format(htmlTemplate, email);
        }

       
    }
}
