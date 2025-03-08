using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;

namespace Project_Jim
{
    public partial class Shop : System.Web.UI.Page
    {
        public string tableHtml;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }
        private void BindProducts()
        {
            string path = HttpContext.Current.Server.MapPath("App_Data/" + "Database1.mdf");//מאתר את מיקום מסד הנתונים מהשורש ועד התקייה בה ממוקם המסד
            string connString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + path + "; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Products";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int productID = (int)reader["product_id"];
                        int productType = (int)reader["product_type"];
                        decimal? productRating = reader["product_rating"] as decimal?;
                        string productName = (string)reader["product_name"];
                        string description = reader["description"] as string;
                        decimal? price = reader["price"] as decimal?;
                        string imageFilename = reader["image_filename"] as string;

                        tableHtml += $@"
                        <div class='product'> 
                            <a href='BuyProduct.aspx' onclick='return PassId({productID})'><img src='{imageFilename}' class='product-image'/></a>
                            <h3 class='product-name'>{productName}</h3>
                            <h3 class='product-price'>{price}</h3>
                        </div>";
                    }

                }
            }
        }
    }
}