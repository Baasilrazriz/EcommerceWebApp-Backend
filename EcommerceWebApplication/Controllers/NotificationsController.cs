using Microsoft.AspNetCore.Mvc;
using EcommerceWebApplication.Service;
using System.Threading.Tasks;

namespace EcommerceWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationsController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

    
        [HttpPost("CreateForRider")]
        public async Task<IActionResult> CreateNotificationForRider(int orderId, int riderId, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be null or empty.");
            }

            try
            {
                await _notificationService.CreateNotificationForRider(orderId, riderId, message);
                return Ok("Notification created successfully.");
            }
            catch (System.Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the notification.");
            }
        }

        // POST: api/Notifications/RespondToOrder
        [HttpPost("RespondToOrder")]
        public async Task<IActionResult> RiderRespondsToOrder(int notificationId, bool accept)
        {
            try
            {
                var result = await _notificationService.RiderRespondsToOrder(notificationId, accept);
                if (result)
                {
                    return Ok("Response recorded successfully.");
                }
                else
                {
                    return NotFound("Notification not found.");
                }
            }
            catch (System.Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while recording the response.");
            }
        }
    }
}
