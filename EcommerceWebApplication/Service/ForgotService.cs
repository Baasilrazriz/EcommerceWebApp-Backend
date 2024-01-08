using Azure.Core;
using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace EcommerceWebApplication.Service
{
    public class ForgotService
    {
        private readonly ApplicationDbContext _context;
        protected EmailService _emailService;
        private const string FromEmail = "baasil86805@gmail.com";
        private const string FromEmailPassword = "jimf vfih dzee cvfn"; // Use App Password if 2FA is enabled


        public ForgotService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DoesUserExist(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException(username);
            }
            var validuser = await _context.ApplicationUsers
             .FromSqlRaw("SELECT * FROM ApplicationUsers WHERE Username = {0}", username)
             .FirstOrDefaultAsync();
            if(validuser == null)
            {
                throw new ArgumentException("User doesnot exist");
            }
            return true;
           
        }
        public async Task<bool> InitiatePasswordRecovery(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
              
                var user = await _context.ApplicationUsers
                    .Where(u => u.UserName == username)
                    .Select(u => new { u.Email })
                    .FirstOrDefaultAsync();

                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var otp = GenerateRandomOTP();

                    await SendOTPViaEmail(user.Email, otp);
                    
                }
            }
     
            return true;
        }
        private string GenerateRandomOTP()
        {
            var random = new Random();
           
            return random.Next(1000, 9999).ToString();
        }
        private async Task<bool> SendOTPViaEmail(string email, string otp)
        {
            
            if(email==null && otp ==null)
            { 
                throw new ArgumentException("content is null");
                
            }
            
                var subject = "Your Password Recovery OTP";
                var content = $"Your OTP for password recovery is: {otp}";

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

                mailMessage.To.Add(email);

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
      
            return true;
               
            
            
        }
    }
}
