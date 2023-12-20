using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CreateCategory _createcategory;
        public CategoryController(ApplicationDbContext context, CreateCategory createcategory)
        {
            _context = context;
            _createcategory = createcategory;
        }
        [HttpGet]

        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.CategoryModels.ToListAsync();
            if (categories.Count > 0)
            {
                return Ok(categories);
            }
            return BadRequest();
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetCategoriesById(int id)
        {
            var categories = await _context.CategoryModels.FindAsync(id);
            if (categories == null)
            {
                return NoContent();
            }
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategories([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            var category = await _createcategory.CreateCategoryAsync(categoryDto);
            if (category == null)
            {
                return BadRequest();
            }
            return Ok(category);
        }

    }
}

