using System;
using System.Data;
using System.IO;
using System.Net.Http.Formatting;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services.Description;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Project_Jim
{
    public partial class Review : System.Web.UI.Page
    {
        public string date,username1,rating,comment,title;
        public string messege;
         void Submit_Click(string username,string date,string rating, string comment,string title)
        {
            string stars="";
            for(int i=0;i<int.Parse(rating);i++) {
                stars += "<i class=\"fa-solid fa-star\" style=\"color: #f6e310;\"></i>";
            }
            XmlDocument xmldoc = new XmlDocument();
            string FilePath = Server.MapPath("~/reviews.xml");
            xmldoc.Load(FilePath);           
            XmlElement reviewelemnt, contentelement, ratingelement,usernameelement,dateelement,titleelement;
            titleelement = xmldoc.CreateElement("title");
            contentelement = xmldoc.CreateElement("comment");
            usernameelement = xmldoc.CreateElement("username");
            ratingelement = xmldoc.CreateElement("rating");
            dateelement = xmldoc.CreateElement("date");
            reviewelemnt = xmldoc.CreateElement("review");
            contentelement.InnerText = comment;
            ratingelement.InnerText = stars;
            usernameelement.InnerText = username;
            dateelement.InnerText = date;
            titleelement.InnerText = title;
            reviewelemnt.AppendChild(titleelement);
            reviewelemnt.AppendChild(usernameelement);
            reviewelemnt.AppendChild(contentelement);
            reviewelemnt.AppendChild(ratingelement);
            reviewelemnt.AppendChild(dateelement);
            xmldoc.DocumentElement.InsertBefore(reviewelemnt, xmldoc.DocumentElement.FirstChild);
            FileStream xmlf = new FileStream(FilePath, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            xmldoc.Save(xmlf);
            xmlf.Close();
            messege = "Your review has been submitted!";
        }
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
                date = DateTime.Now.ToString("HH:mm:ss");
                Submit_Click (username1,date,rating,comment,title);
            }
        }
    }
}
