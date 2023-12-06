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
            optionsBuilder.UseSqlServer("Data Source=shahood-rehan;Initial Catalog=ECommerceWebApp;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
    }

}
