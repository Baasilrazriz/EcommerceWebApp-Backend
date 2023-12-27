using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class RestaurantCategoryModel
    {
        [Key]
        public int RestaurantCategoryId { get; set;}
        public string RestaurantCategoryName { get; set;}
        public string RestaurantCategoryImage { get; set;}

    }
}
