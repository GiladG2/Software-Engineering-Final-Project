using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Project_Jim
{
    public partial class SeeReviews : System.Web.UI.Page
    {
        public string reviews = "";
        public int reviewsall;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] != null)
            {
                if (Session["accesskey"].ToString() == "0")
                    Response.Redirect("Gallery1.aspx");
            }
            else
                Response.Redirect("Gallery1.aspx");

            string FilePath = Server.MapPath("~/reviews.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(FilePath);
            XmlNodeList title = xmldoc.GetElementsByTagName("title");
            XmlNodeList user = xmldoc.GetElementsByTagName("username");
            XmlNodeList comment = xmldoc.GetElementsByTagName("comment");
            XmlNodeList rating = xmldoc.GetElementsByTagName("rating");
            XmlNodeList date = xmldoc.GetElementsByTagName("date");
            reviewsall = user.Count;//השגת כמות התגובות שקיימות 
            for (int i = 0; i < reviewsall; i++)
            {
                reviews += "<div class='review'>";
                reviews += $"<span class='star'>{rating[i].InnerText}</span>";
                reviews += $"<h3>The writer: {user[i].InnerText}</h3>";
                reviews += $"<h3>Title: <strong class='title'>{title[i].InnerText}</strong></h3>";
                reviews += $"<button class='button1' onclick='seer(\"{comment[i].InnerText}\",\"{user[i].InnerText}\")'>See full review</button>";
                reviews += $"<h3 i>The review was written at: {date[i].InnerText}</h3>";
                reviews += "</div>";
            }
        }
    }
}