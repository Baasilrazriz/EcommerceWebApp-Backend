using EcommerceWebApplication.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[HttpPost]
        //[HttpDelete]
    }

}
