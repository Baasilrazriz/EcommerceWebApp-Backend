using EcommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Service
{
    public class UpdateRiderService
    {
        private readonly ApplicationDbContext _context;
        public UpdateRiderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateRiderAsync(int RiderID, RiderDto riderDto)
        {
            
            var rider = await _context.RiderModels.FindAsync(RiderID);
            var riderauth = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == rider.username);


            if (rider == null)
            {
                return false;
            }
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    string password = riderDto.password;
                    string hashedPassword = HashingUtilities.HashPassword(password);


                    rider.Firstname = riderDto.Firstname;
                    rider.Lastname = riderDto.Lastname;
                    rider.Address = riderDto.Address;
                    rider.City = riderDto.City;
                    rider.Country = riderDto.Country;
                    rider.PhoneNumber = riderDto.PhoneNumber;
                    rider.Email = riderDto.Email;
                    rider.Cnic = riderDto.Cnic;
                    rider.Gender = riderDto.Gender;
                    rider.Dob = riderDto.Dob;
                    rider.Age = riderDto.Age;
                    rider.username = riderDto.username;
                    rider.password = hashedPassword;
                    rider.Image = Convert.ToString(riderDto.Image);

                    riderauth.Email = riderDto.Email;
                    riderauth.Password = hashedPassword;

                    _context.Entry(riderauth).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
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
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiderExists(RiderID))
                {
                    return false; 
                }
                else
                {
                    throw;
                }
            }

        }
        private bool RiderExists(int RiderID)
        {
            return _context.RiderModels.Any(a => a.RiderID== RiderID);
        }
    }

}
