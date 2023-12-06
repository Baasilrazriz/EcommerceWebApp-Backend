using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class ApplicationUsers
    {
        [Required]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
