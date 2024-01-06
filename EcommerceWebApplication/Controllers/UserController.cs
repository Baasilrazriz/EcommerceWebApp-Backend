using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUser _createUser;
        private readonly ApplicationDbContext _context;

        public UserController(CreateUser createUser, ApplicationDbContext context)
        {
            _createUser = createUser;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userdata = await _context.UserModels.ToListAsync();
            return Ok(userdata);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _context.UserModels.FirstOrDefaultAsync(a => a.username == username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("Createuser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("You need to fill all the required fields");
            }

            try
            {
                var userdata = await _createUser.CreateNewUserAsync(userDto);
                return Ok(userdata);
            }
            catch (Exception ex)
            {
                
                return BadRequest("An error occurred while creating the user");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto updatedUser)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var updateUserService = new UpdateUserService(_context);
            var isUpdateSuccessful = await updateUserService.UpdateUserAsync(id, updatedUser);

            if (!isUpdateSuccessful)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            var user = await _context.UserModels.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var users = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == user.username);

            // Check if the user exists
            if (user == null)
            {
                return NotFound("Associated user not found.");
            }


            _context.UserModels.Remove(user);
            _context.ApplicationUsers.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

