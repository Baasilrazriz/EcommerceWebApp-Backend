namespace EcommerceWebApplication.Data
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public int IdUser { get; set; }
        public int ProdID { get; set; }
        public string Status { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Tax { get; set; }
    }
}
