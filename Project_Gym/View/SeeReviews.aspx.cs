using iText.StyledXmlParser.Jsoup.Nodes;
using Project_Gym.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Project_Gym.View
{
    public partial class SeeReviews : System.Web.UI.Page
    {
        public string reviews = "";
        public int reviewsall;
        private Xml_Controller xml_Controller = new Xml_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] != null)
            {
                if (Session["accesskey"].ToString() == "0")
                    Response.Redirect("Gallery.aspx");
            }
            else
                Response.Redirect("Gallery.aspx");

            reviews = xml_Controller.DisplayAdminReviews();

            if (Request.Form["submit"] != null)
            {
               xml_Controller.InsertToXml(Request.Form["comment"], Request.Form["usernameToFind"], Request.Form["titleToSend"], Session["fusername"].ToString());
            }

        }
        [WebMethod]
        public static string UpdateStatus(int id, string filePath) // סטטי כדי לא ליצור אובייקט מהמחלקה SeeReview 
        {
            Xml_Controller xml_Controller = new Xml_Controller();
            return xml_Controller.UpdateStatus(id, filePath);
        }
    }
    
}