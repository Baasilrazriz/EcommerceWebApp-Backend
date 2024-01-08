using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Service
{
    public class ForgotResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ApplicationUsers ValidUser { get; set; } 

    }
}
