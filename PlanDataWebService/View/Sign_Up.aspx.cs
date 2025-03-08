using PlanDataWebService.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanDataWebService.View
{
    public partial class Sign_Up : System.Web.UI.Page
    {
        public string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Controller_Users cu = new Controller_Users();
            if (Request.Form["submit"] != null)
            {
                string usernameField = username.Value;
                string passwordField = password.Value;
                if (cu.AddUser(usernameField, passwordField))
                {
                    msg = "Success!";
                }
                else
                    msg = "Username is already taken";
            }
        }
    }
}