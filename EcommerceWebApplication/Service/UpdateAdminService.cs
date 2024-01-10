using EcommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Service
{
    public class UpdateAdminService
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateAdminService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UpdateAdminAsync(int adminId, AdminDto updatedAdminDto)
        {
            // Retrieve the admin by ID from the database
            var admin = await _dbContext.AdminModels.FindAsync(adminId);
            var adminauth = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == admin.username);

            if (admin == null && adminauth == null)
            {
                return false; // Admin not found
            }
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    string password = updatedAdminDto.password;
                    string hashedpassword = HashingUtilities.HashPassword(password);
                   


                    admin.FirstName = updatedAdminDto.FirstName;
                    admin.LastName = updatedAdminDto.LastName;
                    admin.Address = updatedAdminDto.Address;
                    admin.City = updatedAdminDto.City;
                    admin.Country = updatedAdminDto.Country;
                    admin.PhoneNumber = updatedAdminDto.PhoneNumber;
                    admin.Email = updatedAdminDto.Email;
                    admin.Cnic = updatedAdminDto.Cnic;
                    admin.Gender = updatedAdminDto.Gender;
                    admin.Dob = updatedAdminDto.Dob;
                    admin.Age = updatedAdminDto.Age;
                    admin.username = updatedAdminDto.username;
                    admin.password = hashedpassword;
                    admin.Image = updatedAdminDto.Image;

                    
                    adminauth.Password = hashedpassword;
                    adminauth.Email = updatedAdminDto.Email;
                    _dbContext.Entry(adminauth).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
           
        }

        private bool AdminExists(int adminId)
        {
            return _dbContext.AdminModels.Any(a => a.AdminID == adminId);
        }
    }
}
