using EcommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Service
{
    public class UpdateSellerService
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateSellerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> UpdateSellerAsync(int SellerID, SellerDto updatedSellerDto)
        {
            // Retrieve the seller by ID from the database
            var seller = await _dbContext.SellerModels.FindAsync(SellerID);
            var sellerauth = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == seller.username);

            if (seller == null)
            {
                return false;
            }
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    string password = updatedSellerDto.password;
                    string hashedPassword = HashingUtilities.HashPassword(password);
                   

                    seller.FirstName = updatedSellerDto.FirstName;
                    seller.LastName = updatedSellerDto.LastName;
                    seller.Address = updatedSellerDto.Address;
                    seller.City = updatedSellerDto.City;
                    seller.Country = updatedSellerDto.Country;
                    seller.PhoneNumber = updatedSellerDto.PhoneNumber;
                    seller.Email = updatedSellerDto.Email;
                    seller.Cnic = updatedSellerDto.Cnic;
                    seller.Status = updatedSellerDto.Status;
                    seller.Gender = updatedSellerDto.Gender;
                    seller.Dob = updatedSellerDto.Dob;
                    seller.Age = updatedSellerDto.Age;
                    seller.username = updatedSellerDto.username;
                    seller.password = hashedPassword;
                    seller.Image = Convert.ToString(updatedSellerDto.Image);
                    sellerauth.Password = hashedPassword;
                    sellerauth.Email = updatedSellerDto.Email;

                    _dbContext.Entry(sellerauth).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            try
            {
                await _dbContext.SaveChangesAsync();
                return true; // Update successful
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerExists(SellerID))
                {
                    return false; // Admin not found
                }
                else
                {
                    throw;
                }
            }

        }
        private bool SellerExists(int SellerID)
        {
            return _dbContext.SellerModels.Any(a => a.SellerID == SellerID);
        }
    }
}
