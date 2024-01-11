using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EcommerceWebApplication.Service
{
    public class PostOrders
    {
        private readonly ApplicationDbContext _context;
        public NotificationService _notificationservice;
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

                    int assignedRiderId = await GetRiderIdForOrder(order);
                    // Send notification to the rider
                    await _notificationservice.CreateNotificationForRider(order.OrderID, assignedRiderId, "New Order Available");


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
        private async Task<int> GetRiderIdForOrder(OrderModel order)
        {
            var rider = await _context.RiderModels.FirstOrDefaultAsync(u => u.RiderID == 1);

            // Assuming that RiderID is an integer property of RiderModel that represents the rider's ID.
            // Make sure to replace 'RiderID' with the actual property name of RiderModel that holds the rider's ID.
            return rider != null ? rider.RiderID : 0; // Or however you'd like to handle the case when the rider is not found
        }

    }
}
