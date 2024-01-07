using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using Microsoft.Identity.Client;

namespace EcommerceWebApplication.Service
{
    public class PostOrders
    {
        private readonly ApplicationDbContext _context;
        public PostOrders(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<OrderModel> PostOrdersAsync(OrderDto orderDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                RandomNumberGenerator random = new RandomNumberGenerator();
                int orderID = random.GenerateSixDigitNumber();

                try
                {
                    var order = new OrderModel
                    {
                        OrderID = orderID,
                        IdUser = orderDto.IdUser,
                        ProdID = orderDto.ProdID,
                        Status = orderDto.Status,
                        OrderTime = Convert.ToDateTime(orderDto.OrderTime),
                        GrandTotal = orderDto.GrandTotal,
                        TotalPrice = orderDto.TotalPrice,
                        TotalDiscount = orderDto.TotalDiscount,
                        Tax = orderDto.Tax,

                    };
                    await _context.OrderModels.AddAsync(order);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return order;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
