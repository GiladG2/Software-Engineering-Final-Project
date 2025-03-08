using PlanDataWebService.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanDataWebService.Modal
{
    public partial class Log_In : System.Web.UI.Page
    {
        public string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                Controller_Users cu = new Controller_Users();
                string usernameField = username.Value;
                string passwordField =password.Value;
                if (cu.IsValidUser(usernameField, passwordField))
                {
                    msg = "Successfully logged in";
                    Session["username"] = usernameField;
                    Session["password"] = passwordField;
                    Response.Redirect("Home_Page.aspx");
                }
                else
                    msg = "incorret username or password";
            }
        }
    }
}