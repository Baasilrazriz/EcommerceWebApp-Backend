using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Formats.Asn1;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        protected CreateRestaurant _createRestaurant;
        public RestaurantController(ApplicationDbContext context, CreateRestaurant createRestaurant)
        {
            _context = context;
            _createRestaurant= createRestaurant;
        }
        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _context.RestaurantModels.ToListAsync();
            if (restaurants == null)
            {
                return NoContent();
            }
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantsById(int id)
        {
            var restaurant = await _context.RestaurantModels.FindAsync(id);
            if (restaurant == null)
            {
                return NoContent();
            }
            return Ok(restaurant);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDto restaurantDto)
        {
            if (restaurantDto == null)
            {
                return BadRequest();
            }
            var restaurant = await _createRestaurant.CreateRestaurantAsync(restaurantDto);
            if(restaurant != null)
            {
                return Ok(restaurant);
            }
            return BadRequest("Creation Failed");      
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantDto restaurantDto)
        {
            if(id == null || restaurantDto == null)
            {
                return BadRequest();
            }
            var updateRestaurantService = new UpdateRestaurantService(_context);
            var isUpdateSuccessful = await updateRestaurantService.UpdateRestaurantAsync(id, restaurantDto);

            if (!isUpdateSuccessful)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.RestaurantModels.FindAsync(id);
            if (restaurant == null)
            {
                return NoContent();
            }
            _context.RestaurantModels.Remove(restaurant);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
