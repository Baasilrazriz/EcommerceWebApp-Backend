using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class CreateCategory
    {
        private readonly ApplicationDbContext _context;

        public CreateCategory(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryModel> CreateCategoryAsync(CategoryDto categoryDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var category = new CategoryModel
                    {
                        Name = categoryDto.Name,
                        CategoryImage = categoryDto.CategoryImage
                    };
                    await _context.CategoryModels.AddAsync(category);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return category;
                }

                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
