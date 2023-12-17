using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using System.Diagnostics.Contracts;


namespace EcommerceWebApplication.Service
{
    public class CreateSeller
    {
        private readonly ApplicationDbContext _context;
        public CreateSeller(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SellerModel> CreateSellerAsync(SellerDto sellerDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    string password = sellerDto.password;
                    string hashedPassword = HashingUtilities.HashPassword(password);
                    byte[] imageBytes = null;
                    if (!string.IsNullOrWhiteSpace(Convert.ToBase64String(sellerDto.Image)))
                    {
                        imageBytes = Convert.FromBase64String(Convert.ToBase64String(sellerDto.Image));
                    }
                    var newSeller = new SellerModel
                    {
                        FirstName = sellerDto.FirstName,
                        LastName = sellerDto.LastName,
                        Address = sellerDto.Address,
                        City = sellerDto.City,
                        Country = sellerDto.Country,
                        PhoneNumber = sellerDto.PhoneNumber,
                        Email = sellerDto.Email,
                        Cnic = sellerDto.Cnic,
                        Status = "Not verified",
                        Gender = sellerDto.Gender,
                        Dob = sellerDto.Dob,
                        Age = sellerDto.Age,
                        username = sellerDto.username,
                        password = sellerDto.password,
                        Image = imageBytes
                    };


                    var newseller = new ApplicationUsers
                    {
                        UserName = sellerDto.username,
                        Email = sellerDto.Email,
                        Password = hashedPassword,
                        Role = "Seller"
                    };
                    _context.ApplicationUsers.Add(newseller);
                    _context.SellerModels.Add(newSeller);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return newSeller;
                }
                catch
                {
                    transaction.Rollback();
                }
                throw new InvalidOperationException("Seller creation failed.");
            }
        }
    }
}
