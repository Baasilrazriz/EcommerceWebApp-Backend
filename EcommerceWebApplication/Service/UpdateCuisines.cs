using EcommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Service
{
    public class UpdateCuisines
    {
        private readonly ApplicationDbContext _context;
        public UpdateCuisines(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateCuisinesAsync(int id, CuisineDto cuisineDto)
        {
            var res = await _context.CuisineModels.FindAsync(id);

            if (res == null)
            {
                return false;
            }
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    res.Cuisinetype = cuisineDto.Cuisinetype;
                    res.CuisineName = cuisineDto.CuisineName;
                    res.CuisineImage = Convert.ToString(cuisineDto.CuisineImage);
                    res.Price = cuisineDto.Price;
                    res.Description = cuisineDto.Description;
                    res.Stock = cuisineDto.Stock;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
                try
                {
                   await _context.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuisineExists(id))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
            
            
        }
        private bool CuisineExists(int id)
        {
            return _context.CuisineModels.Any(c => c.CuisineId == id);
        }
    }
}