using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class CreateAuthService
    {
        private readonly ApplicationDbContext _context;


        public CreateAuthService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AuthModelGmail> CreateNewUserAsync(AuthGmailDto authGmailDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newuser = new AuthModelGmail
                    {
                        email = authGmailDto.Email,
                        name = authGmailDto.Name,
                        image = Convert.ToString(authGmailDto.Image),
                    };
                    
                    _context.AuthModelGmails.Add(newuser);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return newuser;
                }

                catch (Exception ex)
                {

                    await transaction.RollbackAsync();

                    throw new ApplicationException("An error occurred when creating a new google user.", ex);
                }
            }

        }
        
    }


}
