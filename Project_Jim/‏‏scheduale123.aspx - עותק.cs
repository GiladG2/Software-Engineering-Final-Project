using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Jim
{
    public partial class scheduale123 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] != null)
            {
                if (Session["accesskey"].ToString() != "10")
                    Response.Redirect("LogOut.aspx");
            }
            else
                Response.Redirect("LogOut.aspx");
        }
    }
}