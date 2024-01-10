using Azure.Core;
using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class ForgotService
    {
        private readonly ApplicationDbContext _context;
        protected EmailService _emailService;
        private const string FromEmail = "baasil86805@gmail.com";
        private const string FromEmailPassword = "jimf vfih dzee cvfn"; // Use App Password if 2FA is enabled
        public static Dictionary<string, (string OTP, DateTime Timestamp)> _otpStore =
     new Dictionary<string, (string, DateTime)>(StringComparer.OrdinalIgnoreCase);

        public ForgotService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUsers> GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var user = await _context.ApplicationUsers
                        .FirstOrDefaultAsync(u => u.UserName == username);

            return user; 
        }

        public async Task<bool> InitiatePasswordRecovery(string username, string email)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
            {
                var user = await _context.ApplicationUsers
                    .Where(u => u.UserName == username && u.Email == email)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    var otp = GenerateRandomOTP();
                    await SendOTPViaEmail(user.Email, otp, user.UserName);
                    return true;
                }
            }

            return false;
        }

        private string GenerateRandomOTP()
        {
            var random = new Random();
           
            return random.Next(1000, 9999).ToString();
        }
        private async Task<bool> SendOTPViaEmail(string email, string otp, string username)
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
            _otpStore[username] = (otp, DateTime.UtcNow);
            return true;
               
            
            
        }
        public bool ValidateOTP(string username, string inputOtp)
        {
            if (_otpStore.TryGetValue(username, out var otpInfo))
            {
                bool isValid = otpInfo.OTP == inputOtp && otpInfo.Timestamp.AddMinutes(2) > DateTime.UtcNow;
                _otpStore.Remove(username);
                return isValid;
            }

            return false;
        }

    }
}
