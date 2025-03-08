using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml;
using Aspose.Foundation.UriResolver.RequestResponses;
using System.Net.PeerToPeer;
using Project_Gym.Controller;
using System.Web.Services;

namespace Project_Gym.View
{
    public partial class Mail : System.Web.UI.Page
    {
        public string reviews = "";
        public Xml_Controller xml_Controller = new Xml_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] == null)
                Response.Redirect("Gallery.aspx");
            reviews = xml_Controller.GetResponses(Session["fusername"].ToString());
            if (Request.Form["submit"] != null)
                xml_Controller.InsertToXml(Request.Form["comment"], Request.Form["usernameToFind"], Request.Form["titleToSend"], Session["fusername"].ToString());
        }
        [WebMethod]
        public static string UpdateStatus(int id, string filePath) // סטטי כדי לא ליצור אובייקט מהמחלקה MAIL 
        {
            Xml_Controller xml_Controller = new Xml_Controller();
            return xml_Controller.UpdateStatus(id, filePath);
        }
        
    }
}