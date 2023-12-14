using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUser _createUser;

        public UserController(CreateUser createUser)
        {
            _createUser = createUser;
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
    }
}
