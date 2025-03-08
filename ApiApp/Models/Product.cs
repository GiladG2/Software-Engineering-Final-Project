namespace ApiApp.Models
{
    public class Product
    {
        public int product_id { get; set; }
        public int product_type { get; set; }
        public decimal? product_rating { get; set; }
        public string product_name { get; set; }
        public string description { get; set; }
        public decimal? price { get; set; }
        public string image_filename { get; set; }
    }

}
