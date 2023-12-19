using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserCart/{userId}")]
        public async Task<IActionResult> GetUserCart(int userId)
        {
            var cartItems = await _context.CartModels
                .Where(c => c.UserID == userId)
                .Join(_context.ProductModels,
                      cart => cart.ProductID,
                      product => product.ProductID,
                      (cart, product) => new
                      {
                          CartId = cart.Id,
                          UserId = cart.UserID,
                          ProductId = product.ProductID,
                          ProductName = product.Name,
                          ProductPrice = product.Price,
                          Quantity = cart.Quantity,
                          Description = product.Description,
                          Stock = product.Stock,
                          Discount = product.Discount
                      })
                .ToListAsync();

            return Ok(cartItems);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var carts = await _context.CartModels
                               .Where(c => c.UserID == id)
                               .ToListAsync();
            if (!carts.Any())
            {
                return NotFound();
            }
            foreach (var cart in carts)
            {
                _context.CartModels.Remove(cart);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }




}
