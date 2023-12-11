using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ForgotController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected ForgotService _service;

        public ForgotController(ApplicationDbContext context, ForgotService service)
        {
            _context = context;
            _service = service;
        }
        [HttpPost("ForgotController")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotDto forgotDto)
        {
            // Check if the username exists in the database
            var userExists = await _service.DoesUserExist(forgotDto.Username);
            if (!userExists)
            {
                return NotFound(new { Message = "User not found." });
            }

            // If user exists, initiate the password recovery process
            var recoveryResult = await _service.InitiatePasswordRecovery(forgotDto.Username);
            if (recoveryResult)
            {
                return Ok(new { Message = "Password recovery initiated. Please check your email for further instructions." });
            }

            // If there was an error during the password recovery initiation
            return StatusCode(500, new { Message = "An error occurred while initiating password recovery." });
        }
    }


}

