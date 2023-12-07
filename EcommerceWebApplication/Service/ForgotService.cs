using EcommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Service
{
    public class ForgotService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

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
            return true;
           
        }
        public async Task<bool> InitiatePasswordRecovery(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                // Fetch the user's email from the database using the provided username
                var user = await _context.ApplicationUsers
                    .Where(u => u.UserName == username)
                    .Select(u => new { u.Email })
                    .FirstOrDefaultAsync();

                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    // Generate a random OTP
                    var otp = GenerateRandomOTP();

                    // Send the OTP to the user's email
                    var emailSent = await SendOTPViaEmail(user.Email, otp);
                    return emailSent;
                }
            }
            // Return false if user does not exist or username is null/empty
            return false;
        }
        private string GenerateRandomOTP()
        {
            var random = new Random();
            // Generate a 6 digit OTP
            return random.Next(100000, 999999).ToString();
        }
        private async Task<bool> SendOTPViaEmail(string email, string otp)
        {
            // Use your email service to send the OTP
            // The implementation of this method will depend on how your email service is set up
            try
            {
                var subject = "Your Password Recovery OTP";
                var content = $"Your OTP for password recovery is: {otp}";
                await _emailService.SendEmailAsync(email, subject, content);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                // Consider logging the exception here
                return false;
            }
        }
    }
}
