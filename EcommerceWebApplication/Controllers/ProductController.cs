using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EcommerceWebApplication.Service;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductService createProduct;
        

        public ProductController(ApplicationDbContext context, ProductService _createproduct)
        {
            _context = context;
            createProduct = _createproduct;
     
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.ProductModels.ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            var product = await _context.ProductModels.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProducts([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var product = await createProduct.CreateProductAsync(productDto);
            if (product == null)
            {
                return NoContent();
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducts(int id, [FromBody] ProductDto productDto)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var updateProductService = new UpdateProductService(_context);
            var isUpdateSuccessful = await updateProductService.UpdateProductAsync(id, productDto);
            if (!isUpdateSuccessful)
            {
                return NotFound();
            }
            return Ok(updateProductService);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.ProductModels.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.ProductModels.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
