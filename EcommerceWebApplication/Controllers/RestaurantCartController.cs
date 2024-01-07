using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RestaurantCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        
    }
}
