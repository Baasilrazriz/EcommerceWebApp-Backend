using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestaurantCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserRestaurantCart/{restaurantId}/{userId}")]
        public async Task<IActionResult> GetUserRestaurantCart(int restaurantId, int userId)
        {
            var cartItems = await _context.RestaurantCartModels
                .Where(c => c.UserID == userId && c.RestaurantId == restaurantId)
                .Join(_context.CuisineModels,
                      cart => cart.CuisineId,
                      cuisine => cuisine.CuisineId,
                      (cart, cuisine) => new
                      {
                          CartId = cart.Id,
                          UserId = cart.UserID,
                          RestaurantId = cart.RestaurantId,
                          CuisineId = cuisine.CuisineId,
                          CuisineName = cuisine.CuisineName,
                          CuisineImage = cuisine.CuisineImage,
                          Price = cuisine.Price,
                          Description = cuisine.Description,
                          Stock = cuisine.Stock,
                          Quantity = cart.Quantity
                      })
                .ToListAsync();

            if (!cartItems.Any())
            {
                return NotFound($"No cart items found for user with ID {userId} and restaurant with ID {restaurantId}");
            }

            return Ok(cartItems);
        }
    }
}
