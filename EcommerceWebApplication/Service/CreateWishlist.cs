using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class CreateWishlist
    {
        private readonly ApplicationDbContext _context;
        public CreateWishlist(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<WishlistModel> CreateWishlistAsync(WishlistDto wishlistDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var productID = await _context  .ProductModels.FindAsync(wishlistDto.ProductID);
                var userID = await _context.UserModels.FindAsync(wishlistDto.UserID);
                try
                {
                    var wishlist = new WishlistModel
                    {
                        UserID = wishlistDto.UserID,
                        ProductID = wishlistDto.ProductID
                    };
                    await _context.WishlistModels.AddAsync(wishlist);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return wishlist;

                }

                catch (Exception)
                {

                    transaction.Rollback();
                    throw new ArgumentException("Some error occured");
                }
            }
        }
    }
}
