using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApplication.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
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
        public string username { get; set; }

        [MaxLength(256)]
        public string password { get; set; }
        public string image {  get; set; }


    }   
}
