using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using EcommerceWebApplication.Utilities;

namespace EcommerceWebApplication.Service
{
    public class CreateUser
    {
        private readonly ApplicationDbContext _context;

        public CreateUser(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserModel> CreateNewUserAsync(UserDto userDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newUser = new UserModel
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        address = userDto.address,
                        city = userDto.city,
                        country = userDto.country,
                        phoneNumber = userDto.phoneNumber,
                        email = userDto.email,
                        gender = userDto.gender,
                        dob = userDto.dob,
                        age = userDto.age,
                        role = userDto.role,
                        username = userDto.username,
                        password = PasswordHasher.HashPassword(userDto.password),
                        image = userDto.image
                    };
                    _context.UserModels.Add(newUser);

                    // Save changes in the context to the database
                    await _context.SaveChangesAsync();

                    // If successful, commit the transaction
                    await transaction.CommitAsync();

                    return newUser;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

            }


        }
    }
}
