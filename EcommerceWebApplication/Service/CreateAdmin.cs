using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class CreateAdmin
    {
        private readonly ApplicationDbContext _context;

        public CreateAdmin(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AdminModel> CreateAdminAsync(AdminDto adminDto)
        {   
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    string password = adminDto.password;
                    string hashedPassword = HashingUtilities.HashPassword(password);
                   

                    var newAdmin = new AdminModel
                    {
                        FirstName = adminDto.FirstName,
                        LastName = adminDto.LastName,
                        Address = adminDto.Address,
                        City = adminDto.City,
                        Country = adminDto.Country,
                        PhoneNumber = adminDto.PhoneNumber,
                        Email = adminDto.Email,
                        Cnic = adminDto.Cnic,
                        Gender = adminDto.Gender,
                        Dob = adminDto.Dob,
                        Age = adminDto.Age,
                        username = adminDto.username,
                        password = hashedPassword,
                        Image = Convert.ToString(adminDto.Image),

                    };
                    var newadmin = new ApplicationUsers
                    {
                        UserName = adminDto.username,
                        Email = adminDto.Email,
                        Password = hashedPassword,
                        Role = "Admin"
                    };
                    _context.ApplicationUsers.Add(newadmin);
                    _context.AdminModels.Add(newAdmin);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return newAdmin;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                throw new InvalidOperationException("Admin creation failed.");
            }
        }

    }
}
