using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class CreateRestaurant
    {
        private readonly ApplicationDbContext _context;
        public CreateRestaurant(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RestaurantModel> CreateRestaurantAsync(RestaurantDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var restaurant = new RestaurantModel
                    {
                        RestaurantName= dto.RestaurantName,
                        Image = Convert.ToString(dto.Image),
                        Deliverytime= dto.Deliverytime,
                        Description = dto.Description,
                        Rating = dto.Rating,
                        RestaurantCategory = dto.RestaurantCategory
                    };
                    _context.RestaurantModels.Add(restaurant);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return restaurant;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                throw new InvalidOperationException("Restaurant creation failed");
            }
        }
    }
}
