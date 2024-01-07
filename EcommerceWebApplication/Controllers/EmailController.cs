using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApplication.Controllers
{
    public class EmailController : ControllerBase
    {
        private readonly IWelcomeEmailService _welcomeEmailService;
        public EmailController(IWelcomeEmailService welcomeEmailService)
        {
            _welcomeEmailService = welcomeEmailService;
        }
        [HttpPost("WelcomeController")]
        public async Task<IActionResult> SendWelcomeMessage(string email)
        {
            await _welcomeEmailService.SendWelcomeEmailAsync(email);
            return Ok(email);
        }


    }
}
