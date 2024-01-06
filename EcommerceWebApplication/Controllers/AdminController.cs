using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected CreateAdmin _adminservice;
        public AdminController(ApplicationDbContext context, CreateAdmin adminservice)
        {
            _context = context;
            _adminservice = adminservice;

        }
        [HttpGet]
        public async Task<IActionResult> GetAdmin()
        {
            var admindata = await _context.AdminModels.ToListAsync();
            return Ok(admindata);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetAdminByUsername(string username)
        {
            var admin = await _context.AdminModels.FirstOrDefaultAsync(a => a.username == username);

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminDto adminDto)
        {
            if (adminDto == null)

            {
                throw new ArgumentNullException(nameof(adminDto));
            }
            var result = await _adminservice.CreateAdminAsync(adminDto);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] AdminDto updatedAdmin)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var updateAdminService = new UpdateAdminService(_context);
            var isUpdateSuccessful = await updateAdminService.UpdateAdminAsync(id, updatedAdmin);

            if (!isUpdateSuccessful)
            {
                return NotFound();
            }

            return Ok(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.AdminModels.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            // Find the corresponding user in the ApplicationUsers table
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == admin.username);

            // Check if the user exists
            if (user == null)
            {
                return NotFound("Associated user not found.");
            }

            // Remove the admin and the associated user
            _context.AdminModels.Remove(admin);
            _context.ApplicationUsers.Remove(user);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
