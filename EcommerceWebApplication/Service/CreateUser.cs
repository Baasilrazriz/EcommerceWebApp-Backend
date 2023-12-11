using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using EcommerceWebApplication.Utilities;
using System;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Service
{
    public class CreateUser
    {
        private readonly ApplicationDbContext _context;

        public CreateUser(ApplicationDbContext context)
        {
            _context = context;
        }
        //create user is working perfectly with hashing
        public async Task<UserModel> CreateNewUserAsync(UserDto userDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    byte[] imageBytes = null;
                    if (!string.IsNullOrWhiteSpace(Convert.ToBase64String(userDto.image)))
                    {
                        // Convert the base64-encoded string to a byte array
                        imageBytes = Convert.FromBase64String(Convert.ToBase64String(userDto.image));
                    }

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
                        image = imageBytes // Assign the byte array here
                    };
                    var newuser = new ApplicationUsers
                    {
                        UserName = userDto.username,
                        Email = userDto.email,
                        Password = PasswordHasher.HashPassword(userDto.password),

                    };
                    _context.ApplicationUsers.Add(newuser);
                    _context.UserModels.Add(newUser);

                    // Save changes in the context to the database
                    await _context.SaveChangesAsync();

                    // If successful, commit the transaction
                    await transaction.CommitAsync();

                    return newUser;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any exception occurs
                    await transaction.RollbackAsync();
                    // Throw an application-specific exception or handle the error as appropriate for your application
                    throw new ApplicationException("An error occurred when creating a new user.", ex);
                }
            }
        }
    }
}
