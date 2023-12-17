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

        if (user == null)
        {
            return false; // User not found
        }
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                string password = updatedUserDto.password;
                string hashedPassword = HashingUtilities.HashPassword(password);
                byte[] imageBytes = null;
                if (!string.IsNullOrWhiteSpace(Convert.ToBase64String(updatedUserDto.image)))
                {
                    imageBytes = Convert.FromBase64String(Convert.ToBase64String(updatedUserDto.image));
                }

                // Update the user's properties with the values from updatedUserDto
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
                user.image = updatedUserDto.image;
            }
            catch
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
            if (!UserExists(userId))
            {
                return false; // User not found
            }
            else
            {
                throw;
            }
        }
    }

    private bool UserExists(int userId)
    {
        return _dbContext.UserModels.Any(u => u.UserID == userId);
    }
}

