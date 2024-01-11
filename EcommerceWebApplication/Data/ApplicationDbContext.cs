using EcommerceWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=shahood-rehan;Initial Catalog=ECommerceWebApplication;Integrated Security=True;Trust Server Certificate=True");
        }
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<CartModel> CartModels { get; set; }
        public DbSet<CategoryModel> CategoryModels {  get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<WishlistModel> WishlistModels { get; set; }
        
        public DbSet<AdminModel> AdminModels { get; set; }
        public DbSet<SellerModel> SellerModels { get; set; }
        public DbSet<RiderModel> RiderModels { get; set; }
        public DbSet<RestaurantModel> RestaurantModels { get;set; }
        public DbSet<CuisineModel> CuisineModels { get; set; }
        public DbSet<RestaurantCategoryModel> RestaurantCategoriesModels { get; set; }

        public DbSet<OrderModel> OrderModels { get; set; }
        public DbSet<RestaurantCartModel> RestaurantCartModels { get; set; }
        public DbSet<AuthModelGmail> AuthModelGmails { get; set; }
        public DbSet<RiderNotification> RiderNotifications { get; set; }
    }
}
        

