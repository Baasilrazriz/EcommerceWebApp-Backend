using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpPost("ValidateUsername")]
        public async Task <IActionResult> ValidateUsername([FromBody] ForgotDto forgotDto)
        {
            var userExists = await _service.GetUserByUsername(forgotDto.Username);

            if (userExists == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            return Ok(userExists);
        }

        [HttpPost("InititateRecovery")]
        public async Task<IActionResult> ForgotPassword([FromBody] RecoveryRequestDto recoveryRequest)
        {
            var recoveryResult = await _service.InitiatePasswordRecovery(recoveryRequest.Username, recoveryRequest.Email);

            if (recoveryResult)
            {
                return Ok(new { Message = "Password recovery initiated. Please check your email for further instructions." });
            }
            else
            {
                return BadRequest(new { Message = "Invalid username or email." });
            }
        }

        [HttpPost("ValidateOTP")]
        public IActionResult ValidateOTP([FromBody] OTPValidationDto otpValidation)
        {
            var isValid = _service.ValidateOTP(otpValidation.Username, otpValidation.OTP);

            if (isValid)
            {
                return Ok(new { Message = "OTP is valid." });
            }
            else
            {
                return BadRequest(new { Message = "Invalid or expired OTP." });
            }
        }


    }


}

