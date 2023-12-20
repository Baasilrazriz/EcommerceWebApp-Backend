using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Data
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public int Stock { get; set; }
        public string Image { get; set; }
        public double Discount { get; set; }
        //foreign keys
        public string Categoryname { get; set; }
        public string Vendorname{ get; set; }   

    }
}


