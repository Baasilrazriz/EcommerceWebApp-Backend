using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class CreateRider
    {
        private readonly ApplicationDbContext _context;
        public CreateRider(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RiderModel> CreateRiderAsync(RiderDto riderDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    string password = riderDto.password;
                    string hashedPassword = HashingUtilities.HashPassword(password);
                   
                    var newRider = new RiderModel
                    {
                        Firstname = riderDto.Firstname,
                        Lastname = riderDto.Lastname,
                        Address = riderDto.Address,
                        City = riderDto.City,
                        Country = riderDto.Country,
                        PhoneNumber = riderDto.PhoneNumber,
                        Email = riderDto.Email,
                        Cnic = riderDto.Cnic,
                        Gender = riderDto.Gender,
                        Dob = riderDto.Dob,
                        Age = riderDto.Age,
                        username = riderDto.username,
                        password = riderDto.password,
                        Image = Convert.ToString(riderDto.Image)
                    };
                    var newrider = new ApplicationUsers
                    {
                        UserName = riderDto.username,
                        Email = riderDto.Email,
                        Password = hashedPassword,
                        Role = "Seller"
                    };
                    _context.ApplicationUsers.Add(newrider);
                    _context.RiderModels.Add(newRider);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return newRider;
                }
                catch
                {
                    transaction.Rollback();
                }
                throw new InvalidOperationException("Rider creation failed.");
            }
        }
    }
}
