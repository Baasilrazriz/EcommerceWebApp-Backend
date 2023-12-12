using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using EcommerceWebApplication.Utilities;
using EcommerceWebApplication.Models;
using System;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Service
{
    public class LoginService
    {
        private readonly ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
            // Initialize key and IV when the service is created
            
        }

        public async Task<LoginResult> ValidateUserAsync(LoginDto loginDto)
        {
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserName == loginDto.Username);

            if (user != null)
            {
                if (user.Password == loginDto.Password)
                {
                    return new LoginResult { IsSuccess = true, ValidUser = user };
                }
            }

            return new LoginResult { IsSuccess = false, ErrorMessage = "Invalid password" };
        }
    }
}
