using EcommerceWebApplication.Data;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class NotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateNotificationForRider(int orderId, int riderId, string message)
        {
            var notification = new RiderNotification
            {
                OrderId = orderId,
                RiderId = riderId,
                Message = message
            };

            _context.RiderNotifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RiderRespondsToOrder(int notificationId, bool accept)
        {
            var notification = await _context.RiderNotifications.FindAsync(notificationId);
            if (notification == null)
            {
                return false;
            }

            notification.Accepted = accept;
            await _context.SaveChangesAsync();
            return true;
        }

    }


}

