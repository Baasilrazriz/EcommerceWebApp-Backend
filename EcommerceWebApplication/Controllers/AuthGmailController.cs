using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthGmailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CreateAuthService _createAuthService;
        public AuthGmailController(ApplicationDbContext context, CreateAuthService createAuthService)
        {
            _context = context;
            _createAuthService = createAuthService;     
        }
        [HttpGet]
        public async Task<IActionResult> GetGoogleUsers()
        {
            var googleusers = await _context.AuthModelGmails.ToListAsync();
            if(googleusers.Count > 0)
            {
                return Ok(googleusers);
            }
            return NotFound();
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUsersByUsername(string name)
        {
            var user = await _context.AuthModelGmails.FirstOrDefaultAsync(a => a.name== name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] AuthGmailDto authGmailDto)
        {
            if (authGmailDto == null)
            {
                return BadRequest("You need to fill all the required fields");
            }

            try
            {
                var userdata = await _createAuthService.CreateNewUserAsync(authGmailDto);
                return Ok(userdata);
            }
            catch (Exception ex)
            {

                return BadRequest("An error occurred while creating the user");
            }
        }

    }
}
