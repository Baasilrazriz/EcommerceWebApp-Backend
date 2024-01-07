using EcommerceWebApplication.Data;
using EcommerceWebApplication.Migrations;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class CreateCuisines
    {
        private readonly ApplicationDbContext _context;
        public CreateCuisines(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CuisineModel> CreateCuisinesAsync(CuisineDto cuisineDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var model = new CuisineModel
                    {
                      
                        RestaurantId = cuisineDto.RestaurantId,
                        Cuisinetype = cuisineDto.Cuisinetype,
                        CuisineName = cuisineDto.CuisineName,
                        CuisineImage = Convert.ToString(cuisineDto.CuisineImage),
                        Price = cuisineDto.Price,
                        Description = cuisineDto.Description,
                        Stock = cuisineDto.Stock,
                    };
                    _context.CuisineModels.Add(model);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return model;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("An error occured!");
                    
                }
            }
        }
    }
}
