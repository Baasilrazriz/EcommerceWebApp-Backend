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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRider(int id)
        {

            var rider = await _context.RiderModels.FindAsync(id);

            if (rider == null)
            {
                return NotFound();
            }

            _context.RiderModels.Remove(rider);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


