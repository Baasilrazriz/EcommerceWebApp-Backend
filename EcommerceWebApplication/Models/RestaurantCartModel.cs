using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class RestaurantCartModel
    {
        [Key]
        public int Id { get; set; }
        public int CuisineId { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public int RestaurantId { get; set; }
    }
}
