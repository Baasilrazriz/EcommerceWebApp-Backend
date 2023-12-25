using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class WishlistModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }

    }
}
