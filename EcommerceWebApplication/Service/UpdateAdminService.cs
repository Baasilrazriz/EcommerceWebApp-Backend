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

            if (admin == null)
            {
                return false; // Admin not found
            }

       
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
            admin.password = updatedAdminDto.password;
            admin.Image = updatedAdminDto.Image;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true; // Update successful
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(adminId))
                {
                    return false; // Admin not found
                }
                else
                {
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
