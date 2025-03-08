using Project_Gym.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Project_Gym.View
{
    public partial class Send_Review : System.Web.UI.Page
    {
        public string date, username1, rating, comment, title;
        public string messege;
        public Xml_Controller xml_Controller = new Xml_Controller();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] == null)
                Response.Redirect("Log_in.aspx");
            if (Request.Form["submit"] != null)
            {
                username1 = Session["fusername"].ToString();
                rating = Request.Form["rating"].ToString();
                comment = Request.Form["comment"].ToString();
                comment = string.Join(" ", comment.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                title = Request.Form["title"].ToString();
                DateTime currentDate = DateTime.Now;
                date = currentDate.ToString("dd/MM/yyyy");
                xml_Controller.InsertAdminReviews(username1, date, rating, comment, title);
            }
        }
    }
}