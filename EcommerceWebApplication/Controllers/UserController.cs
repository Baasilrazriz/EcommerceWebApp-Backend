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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.UserModels.FindAsync(id);

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
                // You can log the exception here
                return BadRequest("An error occurred while creating the user");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto updatedUser)
        {
            if (id != updatedUser.UserID)
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

            _context.UserModels.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

