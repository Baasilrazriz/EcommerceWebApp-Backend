using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceWebApplication.Models;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Logging.Abstractions;

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
        [HttpPost]
        public async Task<IActionResult> PostCartItem(CartItemDto cartItemDto)
        {
            if (cartItemDto == null)
            {
                return BadRequest();
            }
            else
            {
                var cart = new CartModel
                {
                    ProductID = cartItemDto.ProductID,
                    UserID = cartItemDto.UserID,
                    Quantity = cartItemDto.Quantity,
                };
                _context.CartModels.Add(cart);
                await _context.SaveChangesAsync();
                return Ok(cart);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, CartItemDto cartItemDto)
        {
            if (cartItemDto == null)
            {
                return BadRequest();
            }
            var cart = await _context.CartModels.FindAsync(id);
            try
            {
                if (cart != null)
                {
                    cart.Quantity = cartItemDto.Quantity;
                    await _context.SaveChangesAsync();

                }
                return Ok(cart);
            }
            catch
            {
                return BadRequest();
            }
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
        [HttpDelete("DeleteCartByCartID/{id}")]
        public async Task<IActionResult> DeleteCartByCartID(int id)
        {
            var cart = await _context.CartModels.FindAsync(id);
            if (cart != null)
            {
                _context.CartModels.Remove(cart);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            return NotFound();
        }
    }




}
