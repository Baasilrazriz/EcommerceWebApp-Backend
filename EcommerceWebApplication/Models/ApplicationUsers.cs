using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class ApplicationUsers
    {
        [Required]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [MaxLength(256)]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
