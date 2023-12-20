using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }  
        public string Description { get; set; }
        
        public int Stock { get; set; }
        public string Image { get; set; }
        public double Discount { get; set; }
        //foreign keys
        public int CategoryID { get; set; }
        public int VendorID { get; set; }

    }
}
