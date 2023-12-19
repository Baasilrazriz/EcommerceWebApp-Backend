using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using EcommerceWebApplication.Utilities;
using System;
using System.Security.Cryptography;
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
                    string password = userDto.password;
                    string hashedPassword = HashingUtilities.HashPassword(password);
                    

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
                        username = userDto.username,
                        password = hashedPassword,
                        image = Convert.ToString(userDto.image) // Assign the byte array here
                    };
                    var newuser = new ApplicationUsers
                    {
                        UserName = userDto.username,
                        Email = userDto.email,
                        Password= hashedPassword,
                        Role = "User"   
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
                   
                    await transaction.RollbackAsync();
                 
                    throw new ApplicationException("An error occurred when creating a new user.", ex);
                }
            }
        }
    }
}
