using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

namespace EcommerceWebApplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CuisineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CreateCuisines _createCuisines;
        public CuisineController(ApplicationDbContext context, CreateCuisines createCuisines)
        {
            _context = context;
            _createCuisines = createCuisines;
        }
        [HttpGet]
        public async Task<IActionResult> GetCuisines()
        {
            var cuisines = await _context.CuisineModels.ToListAsync();
            if (cuisines != null)
            {
                return Ok(cuisines);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCuisines([FromBody] CuisineDto cuisineDto)
        {
            if (cuisineDto == null)
            {
                return NoContent();
            }
            var create = await _createCuisines.CreateCuisinesAsync(cuisineDto);
            if (create != null)
            {
                return Ok(create);
            }
            return BadRequest("Error occured");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuisines(int id, [FromBody] CuisineDto cuisineDto)
        {
            if (id == null || cuisineDto == null)
            {
                return BadRequest();
            }
            var updateCuisineService = new UpdateCuisines(_context);
            var isUpdateSuccessful = await updateCuisineService.UpdateCuisinesAsync(id, cuisineDto);

            if (!isUpdateSuccessful)
            {
                return NotFound();
            }

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteCuisine (int id)
        {
            var cuisine = await _context.CuisineModels.FindAsync(id);
            if (cuisine != null)
            {
                _context.Remove(cuisine);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }


    }
}
