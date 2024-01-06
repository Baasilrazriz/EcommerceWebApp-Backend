using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RiderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected CreateRider _createRider;
        public RiderController(ApplicationDbContext context, CreateRider createRider)
        {
            _context = context;
            _createRider = createRider;
        }
        [HttpGet]
        public async Task<IActionResult> GetRider()
        {
            var riderdata = await _context.RiderModels.ToListAsync();
            return Ok(riderdata);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetRiderByUsername(string username)
        {
            var rider = await _context.RiderModels.FirstOrDefaultAsync(a => a.username == username);

            if (rider == null)
            {
                return NotFound();
            }

            return Ok(rider);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRider([FromBody] RiderDto riderDto)
        {
            if (riderDto == null)

            {
                throw new ArgumentNullException(nameof(riderDto));
            }
            var result = await _createRider.CreateRiderAsync(riderDto);
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
        public async Task<IActionResult> UpdateRider(int id, [FromBody] RiderDto updatedRider)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var updateRiderService = new UpdateRiderService(_context);
            var isUpdateSuccessful = await updateRiderService.UpdateRiderAsync(id, updatedRider);

            if (!isUpdateSuccessful)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRider(int id)
        {

            var rider = await _context.RiderModels.FindAsync(id);

            if (rider == null)
            {
                return NotFound();
            }
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == rider.username);

            // Check if the user exists
            if (user == null)
            {
                return NotFound("Associated user not found.");
            }

            _context.RiderModels.Remove(rider);
            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


