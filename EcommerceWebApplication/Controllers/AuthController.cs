using Microsoft.AspNetCore.Mvc;
using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using EcommerceWebApplication.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        protected LoginService _loginService;
        public AuthController(ApplicationDbContext context, LoginService loginService)
        {
            _context = context;
            _loginService = loginService;

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _loginService.ValidateUserAsync(loginDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }

        }
    }

}

