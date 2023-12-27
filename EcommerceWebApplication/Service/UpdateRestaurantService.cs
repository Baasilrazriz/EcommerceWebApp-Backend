using EcommerceWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Service
{
    public class UpdateRestaurantService
    {
        private readonly ApplicationDbContext _context;
        public UpdateRestaurantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateRestaurantAsync(int id, RestaurantDto restaurantDto)
        {
            var res = await _context.RestaurantModels.FindAsync(id);

            if (res == null)
            {
                return false;
            }
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    res.RestaurantName = restaurantDto.RestaurantName;
                    res.Image = Convert.ToString(restaurantDto.Image);
                    res.Deliverytime = restaurantDto.Deliverytime;
                    res.Description = restaurantDto.Description;
                    res.Rating = restaurantDto.Rating;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
                try
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(id))
                    {
                        return false; // Admin not found
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
        private bool RestaurantExists(int id)
        {
            return _context.RestaurantModels.Any(r => r.RestaurantId == id);
        }
    }
}
