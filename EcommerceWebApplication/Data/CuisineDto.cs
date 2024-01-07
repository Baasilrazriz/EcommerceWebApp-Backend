namespace EcommerceWebApplication.Data
{
    public class CuisineDto
    {
     
        public int RestaurantId { get; set; }
        public string Cuisinetype { get; set; }
        public string CuisineName { get; set; }
        public string CuisineImage { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
