using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Jim.ShopFetch
{
    public class Product
    {
        public int ProductID { get; set; }
        public int ProductType { get; set; }
        public decimal? ProductRating { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ImageFilename { get; set; }
    }

}