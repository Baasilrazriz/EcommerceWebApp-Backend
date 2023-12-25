using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CreateWishlist _createWishlist;
        public WishlistController(ApplicationDbContext context, CreateWishlist createWishlist)
        {
            _context = context;
            _createWishlist = createWishlist;
        }
        [HttpGet("GetUserWishlist/{userId}")]
        public async Task<IActionResult> GetUserWishlist(int userId)
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
        [HttpPost]
        public async Task<IActionResult> CreateUserWishlist([FromBody] WishlistDto wishlistDto)
        {
            if (wishlistDto == null)
            {
                return BadRequest();
            }
            var product = await _createWishlist.CreateWishlistAsync(wishlistDto);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromWishlist(int productID, int UserId)
        {
            var wishlist = await _context.WishlistModels
                .SingleOrDefaultAsync(w => w.ProductID == productID && w.UserID == UserId);


            if (wishlist == null)
            {
                return NotFound();
            }

            _context.WishlistModels.Remove(wishlist);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content is typically returned for successful DELETE operations
        }




    }
}
