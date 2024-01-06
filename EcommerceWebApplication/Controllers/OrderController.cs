using EcommerceWebApplication.Data;
using EcommerceWebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace EcommerceWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PostOrders _postOrders;
        public OrderController(ApplicationDbContext context, PostOrders postOrders)
        {
            _context = context;
            _postOrders = postOrders;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.OrderModels.ToListAsync();
            if (orders != null)
            {
                return Ok(orders);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersByID(int id)
        {
            var orderbyid = await _context.OrderModels.FindAsync(id);
            if (orderbyid != null)
            {
                return Ok(orderbyid);
            }
            return BadRequest("Order by ID donot exist");
        }
        [HttpPost]
        public async Task<IActionResult> PostOrders([FromBody] OrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest();
            }
            var order = await _postOrders.PostOrdersAsync(orderDto);
            if (order != null)
            {
                return Ok("Order Posted Successfully");
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UdpateOrder(int id, OrderDto orderDto)
        {
            var order = await _context.OrderModels.FindAsync(id);
            if (order != null)
            {
                order.Status = orderDto.Status;
                await _context.SaveChangesAsync();
                return Ok(order);
            }
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.OrderModels.FindAsync(id);
            if (order != null)
            {
                _context.OrderModels.Remove(order);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest("The specified order does not exist");
        }
    }

}
