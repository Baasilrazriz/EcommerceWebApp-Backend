using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
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
        public CreateRestaurantCart _create;

        public RestaurantCartController(ApplicationDbContext context, CreateRestaurantCart create)
        {
            _context = context;
            _create = create;
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
        [HttpPost]
        public async Task<IActionResult> CreateRestaurantCart([FromBody] RestaurantCartDto restaurantCartDto)
        {
            if (restaurantCartDto == null)
            {
                return BadRequest();
            }
            var cart = await _create.CreateRestaurantCartAsync(restaurantCartDto);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurantCart(int id, RestaurantCartDto restaurantCartDto)
        {
            if (restaurantCartDto == null)
            {
                return BadRequest();
            }
            var cart = await _context.RestaurantCartModels.FindAsync(id);
            try
            {
                if (cart != null)
                {
                    cart.Quantity = restaurantCartDto.Quantity;
                    await _context.SaveChangesAsync();

                }
                return Ok(cart);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-cart/{cartid}")]
        public async Task<IActionResult> DeleteByCartID(int cartid)
        {
            var cart = await _context.RestaurantCartModels.FindAsync(cartid);
            if (cart != null)
            {
                _context.RestaurantCartModels.Remove(cart);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest("The specified cartID does not exist");
        }


        [HttpDelete("{restaurantid}/{userid}")]
        public async Task<IActionResult> DeleteByRestaurantAndUserID(int restaurantid, int userid)
        {

            var carts = await _context.RestaurantCartModels
                                      .Where(c => c.RestaurantId == restaurantid && c.UserID == userid)
                                      .ToListAsync();

            if (carts.Any())
            {

                _context.RestaurantCartModels.RemoveRange(carts);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return NotFound("No carts found with the specified restaurant ID and user ID.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartByUserid(int id)
        {
            var carts = await _context.RestaurantCartModels
                               .Where(c => c.UserID == id)
                               .ToListAsync();
            if (!carts.Any())
            {
                return NotFound();
            }
            foreach (var cart in carts)
            {
                _context.RestaurantCartModels.Remove(cart);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
