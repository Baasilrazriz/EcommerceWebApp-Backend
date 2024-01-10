using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class AuthModelGmail
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string image { get; set; }

    }
}
