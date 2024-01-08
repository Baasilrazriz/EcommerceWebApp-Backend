using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Service
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductModel> CreateProductAsync(ProductDto productDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            var category = await _context.CategoryModels
                           .FirstOrDefaultAsync(c => c.Name == productDto.Categoryname);

            var vendor = await _context.SellerModels
                .FirstOrDefaultAsync(v => v.FirstName == productDto.Vendorname);

            if (category == null)
            {
                
                throw new ArgumentException("You should select a valid category");
            }
            if (vendor == null)
            {
                throw new ArgumentException("You should enter a registered vendor");
            }

            try
            {
                
                var newProduct = new ProductModel
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Description = productDto.Description,
                    Stock = productDto.Stock,
                    Image = Convert.ToString(productDto.Image),
                    Discount = productDto.Discount,
                    CategoryID = category.CategoryID,
                    VendorID = vendor.SellerID
                };

                await _context.ProductModels.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return newProduct;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
