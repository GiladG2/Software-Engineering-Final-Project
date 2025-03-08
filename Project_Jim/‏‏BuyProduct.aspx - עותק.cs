using Project_Jim.ShopFetch;
using System;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace Project_Jim
{
    public partial class BuyProduct : System.Web.UI.Page
    {
        public string productId, image, name, description,stars;
        public int price;
        public double rating;

        protected void Page_Load(object sender, EventArgs e)
        {
            productId = Request.QueryString["id"];
            if (!IsPostBack)
            {
                DisplayProduct();
              stars= HowMuchStars(rating);
            }
        }
        
        void DisplayProduct()
        {
            string path = HttpContext.Current.Server.MapPath("App_Data/" + "Database1.mdf");
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + ";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string sqlQuery = $"SELECT * FROM Products WHERE product_id='{productId}'";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    productId = reader["product_id"].ToString();
                    image = reader["image_filename"].ToString();
                    name = reader["product_name"].ToString();
                    description = reader["description"].ToString();
                    price = Convert.ToInt32(reader["price"]);
                    rating = Convert.ToDouble(reader["product_rating"]);
                }
                else
                {
                    // The product with the given ID is not found
                    // Redirect to a 404 not found page
                    Response.Redirect("/404NotFoundPage.aspx");
                }
            }
        }
        string HowMuchStars(double rating)
        {
            string total = "";
            string onestar = "<i class=\"fa-sharp fa-solid fa-star\"></i>";
            string halfstar = "<i class=\"fa-regular fa-star-half-stroke\"></i>";
            while (rating >= 1)
            {
                total += onestar;
                rating--;
            }
            if (rating != 0)
                total += halfstar;
            return total;
        }
    }
}


