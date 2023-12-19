using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetUserWishlist/{userId}")]
        public async Task<IActionResult> GetUserWishlist(string userId)
        {
            var wishlistItems = await _context.WishlistModels
                .Where(w => w.UserID == userId)
                .Join(_context.ProductModels,
                      wishlist => wishlist.ProductID,
                      product => product.ProductID,
                      (wishlist, product) => new
                      {
                          WishlistId = wishlist.Id,
                          UserId = wishlist.UserID,
                          ProductId = product.ProductID,
                          ProductName = product.Name,
                          ProductPrice = product.Price,
                          Description = product.Description,
                          Stock = product.Stock,
                          Image = product.Image,
                          Discount = product.Discount,
                          CategoryID = product.CategoryID,
                          VendorID = product.VendorID
                      })
                .ToListAsync();

            return Ok(wishlistItems);
        }

    }
}
