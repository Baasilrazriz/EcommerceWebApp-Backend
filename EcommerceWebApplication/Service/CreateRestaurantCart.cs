using EcommerceWebApplication.Data;
using EcommerceWebApplication.Migrations;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class CreateRestaurantCart
    {
        private readonly ApplicationDbContext _context;
        public CreateRestaurantCart(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RestaurantCartModel> CreateRestaurantCartAsync(RestaurantCartDto restaurantCartDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var cart = new RestaurantCartModel
                    {
                        CuisineId = restaurantCartDto.CuisineId,
                        UserID = restaurantCartDto.UserID,
                        Quantity = restaurantCartDto.Quantity,
                        RestaurantId = restaurantCartDto.RestaurantId
                    };
                    _context.RestaurantCartModels.Add(cart);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return cart;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
