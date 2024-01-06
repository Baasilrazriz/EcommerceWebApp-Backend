using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected CreateSeller _sellerservice;
        public SellerController(ApplicationDbContext context, CreateSeller sellerservice)
        {
            _context = context;
            _sellerservice = sellerservice;

        }
        [HttpGet]
        public async Task<IActionResult> GetSeller()
        {
            var sellerdata = await _context.SellerModels.ToListAsync();
            return Ok(sellerdata);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetSellerByUsername(string username)
        {
            var seller = await _context.SellerModels.FirstOrDefaultAsync(a => a.username == username);

            if (seller == null)
            {
                return NotFound();
            }

            return Ok(seller);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSeller([FromBody] SellerDto sellerDto)
        {
            if (sellerDto == null)

            {
                throw new ArgumentNullException(nameof(sellerDto));
            }
            var result = await _sellerservice.CreateSellerAsync(sellerDto);
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
        public async Task<IActionResult> UpdateSeller(int id, [FromBody] SellerDto updatedSeller)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var updateSellerService = new UpdateSellerService(_context);
            var isUpdateSuccessful = await updateSellerService.UpdateSellerAsync(id, updatedSeller);

            if (!isUpdateSuccessful)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {

            var seller = await _context.SellerModels.FindAsync(id);

            if (seller == null)
            {
                return NotFound();
            }
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == seller.username);

            // Check if the user exists
            if (user == null)
            {
                return NotFound("Associated user not found.");
            }


            _context.SellerModels.Remove(seller);
            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
