using ControllersProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Gym.View
{
    public partial class Delete : System.Web.UI.Page
    {
        string tbl = "tblUsers";
        Users_Controller cu = new Users_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["fusername"] == null)//בדיקה האם הגולש מנסה למחוק משתמש שבכלל לא מחובר מה שיגרום לשגיאה
                Response.Redirect("Landing_Page.aspx");//הוצאה החוצה מתהליך המחיקה אל דף הנחיתה
            Session.Abandon();//sessionניתוק ה 
            string username = Session["fusername"].ToString();//שנוצר כשהמשתמש התתחבר session לקיחת שם המשתמש מה
            bool check = cu.DeleteUser(username);
            Response.Redirect("Landing_Page.aspx");
        }
    }
}