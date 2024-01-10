using EcommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class UpdateUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateUserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> UpdateUserAsync(int userId, UserDto updatedUserDto)
    {
        // Retrieve the user by ID from the database
        var user = await _dbContext.UserModels.FindAsync(userId); 
        var userauth = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == user.username);

        if (user == null || userauth == null)
        {
            return false; // User not found
        }

        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                string password = updatedUserDto.password;
                string hashedPassword = HashingUtilities.HashPassword(password);
                
                user.FirstName = updatedUserDto.FirstName;
                user.LastName = updatedUserDto.LastName;
                user.address = updatedUserDto.address;
                user.city = updatedUserDto.city;
                user.country = updatedUserDto.country;
                user.phoneNumber = updatedUserDto.phoneNumber;
                user.email = updatedUserDto.email;
                user.gender = updatedUserDto.gender;
                user.dob = updatedUserDto.dob;
                user.age = updatedUserDto.age;
                user.username = updatedUserDto.username;
                user.password = updatedUserDto.password;
                user.image = Convert.ToString(updatedUserDto.image);
                userauth.Email= updatedUserDto.email;
                userauth.UserName = updatedUserDto.username;
                userauth.Password = hashedPassword;

                _dbContext.Entry(userauth).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

    }

    private bool UserExists(int userId)
    {
        return _dbContext.UserModels.Any(u => u.UserID == userId);
    }
}

