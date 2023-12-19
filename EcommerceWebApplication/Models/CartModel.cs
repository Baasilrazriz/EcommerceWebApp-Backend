using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class CartModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }

    }
}
