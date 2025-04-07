using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanDataWebService.View
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();//session סגירת ה 
            Response.Redirect("Home.aspx");//החזרה לדף הנחיתה
        }
    }
}