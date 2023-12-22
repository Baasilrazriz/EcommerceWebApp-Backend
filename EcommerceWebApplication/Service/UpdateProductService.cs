using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceWebApplication.Service
{
    public class UpdateProductService
    {
        private readonly ApplicationDbContext _context;
        public UpdateProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _context.ProductModels.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    product.Price = productDto.Price;
                    product.Description = productDto.Description;
                    product.Stock = productDto.Stock;
                    product.Image = Convert.ToString(productDto.Image);
                    product.Discount = productDto.Discount;
                    await transaction.CommitAsync();
                    await _context.SaveChangesAsync();
                    
                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
                return true;

            }

        }
    }
}
