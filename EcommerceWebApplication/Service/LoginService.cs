using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Service
{
    public class LoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResult> ValidateUserAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }
            var validuser = await _context.ApplicationUsers
             .FromSqlRaw("SELECT * FROM ApplicationUsers WHERE Username = {0} AND Password = {1}", loginDto.Username, loginDto.Password)
             .FirstOrDefaultAsync();

            if(validuser == null) 
            {
            return new LoginResult { IsSuccess = false, ErrorMessage = "Invalid Login Attempt."};
            }

            return new LoginResult { IsSuccess = true, ValidUser = validuser };

        }


    }
}
