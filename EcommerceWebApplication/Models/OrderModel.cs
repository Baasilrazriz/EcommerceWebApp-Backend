namespace EcommerceWebApplication.Models
{
    public class OrderModel
    {
        public int OrderID{ get; set; }
        public int UserID { get; set; }
        public string Status { get; set; }
        public DateTime OrderTime{ get; set;}
        public decimal GrandTotal { get; set;}
        public decimal TotalPrice { get; set;}
        public decimal TotalDiscount { get; set;}
        public decimal Tax { get; set;}

    }
}
