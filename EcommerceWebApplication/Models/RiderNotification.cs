namespace EcommerceWebApplication.Models
{
    public class RiderNotification
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int RiderId { get; set; }
        public string Message { get; set; }
        public bool? Accepted { get; set; }
    }
}
