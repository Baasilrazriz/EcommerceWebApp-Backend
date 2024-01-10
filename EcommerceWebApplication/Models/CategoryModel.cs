using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string CategoryImage {  get; set; }

    }
}
