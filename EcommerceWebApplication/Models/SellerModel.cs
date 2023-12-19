using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class SellerModel
    {
        [Key]
        public int SellerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Cnic { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public int Age { get; set; }
        public string username { get; set; }
        [MaxLength(256)]
        public string password { get; set; }
        public string Image { get; set; }
    }
}
