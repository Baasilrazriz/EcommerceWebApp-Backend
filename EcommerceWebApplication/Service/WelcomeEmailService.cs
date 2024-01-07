using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Service
{
    public class WelcomeEmailService : IWelcomeEmailService
    {
        private const string FromEmail = "baasil86805@gmail.com";
        private const string FromEmailPassword = "jimf vfih dzee cvfn"; // Use App Password if 2FA is enabled
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
            // A simplified HTML template for debugging
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

            // Replace placeholder with email
            return string.Format(htmlTemplate, email);
        }

        //        private string GetEmailBody(string email)
        //        {

        //            var htmlTemplate = @"<!DOCTYPE html>
        //<html>
        //  <head>
        //    <title>Welcome to Door Dash </title>
        //    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
        //    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
        //    <style type=""text/css"">
        //      /* Base */
        //      body {
        //        margin: 0;
        //        padding: 0;
        //        min-width: 100%;
        //        font-family: Arial, sans-serif;
        //        font-size: 16px;
        //        line-height: 1.5;
        //        background-color: #FAFAFA;
        //        color: #222222;
        //      }
        //      a {
        //        color: #000;
        //        text-decoration: none;
        //      }
        //      h1 {
        //        font-size: 24px;
        //        font-weight: 700;
        //        line-height: 1.25;
        //        margin-top: 0;
        //        margin-bottom: 15px;
        //        text-align: center;
        //      }
        //      p {
        //        margin-top: 0;
        //        margin-bottom: 24px;
        //      }
        //      table td {
        //        vertical-align: top;
        //      }
        //      /* Layout */
        //      .email-wrapper {
        //        max-width: 600px;
        //        margin: 0 auto;
        //      }
        //      .email-header {
        //        background-color: #0070f3;
        //        padding: 24px;
        //        color: #ffffff;
        //      }
        //      .email-body {
        //        padding: 24px;
        //        background-color: #ffffff;
        //      }
        //      .email-footer {
        //        background-color: #f6f6f6;
        //        padding: 24px;
        //      }
        //    </style>
        //  </head>
        //  <body>
        //    <div class='email-wrapper'>
        //      <div class='email-header'>
        //        <h1>Welcome to DoorDash</h1>
        //      </div>
        //      <div class='email-body'>
        //        <p>Hello {0},</p>
        //        <p>Welcome back to DoorDash! It's great to have you with us again. As you navigate through our diverse range of products, we hope you find everything you're looking for - and more. Remember, we're here to make your shopping experience as seamless and enjoyable as possible.  </p>
        //        <p>If you did not register for a DoorDash account, please ignore this email.</p>
        //      </div>
        //      <div class='email-footer'>
        //        <p>If you have any questions, please don't hesitate to contact us at <a href='mailto:doordash@gmail/.com'>doordash@gmail/.com</a></p>
        //      </div>
        //    </div>
        //  </body>
        //</html>";

        //            // Replace placeholders with actual values
        //            return string.Format(htmlTemplate, email);
        //        }
        //    }
    }
}
