using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetProductsByCategory/{categoryName}")]
        public async Task<IActionResult> GetProductsByCategory(string categoryName)
        {
            var products = await _context.ProductModels
                .Join(_context.CategoryModels,
                      product => product.CategoryID,
                      category => category.CategoryID,
                      (product, category) => new { Product = product, Category = category })
                .Where(pc => pc.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase))
                .Select(pc => new
                {
                    ProductId = pc.Product.ProductID,
                    Name = pc.Product.Name,
                    Price = pc.Product.Price,
                    Description = pc.Product.Description,
                    Stock = pc.Product.Stock,
                    Image = pc.Product.Image,
                    Discount = pc.Product.Discount,
                    Category = pc.Category.Name,
                    VendorID = pc.Product.VendorID
                })
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound("No products found for the given category.");
            }

            return Ok(products);
        }
    }
}
