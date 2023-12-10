using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public int age { get; set; }
        public string role { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public byte[] image {  get; set; }


    }
}
