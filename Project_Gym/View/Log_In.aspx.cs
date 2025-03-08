using ControllersProject.Controller;
using Project_Gym.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Gym.View
{
    public partial class Log_In1 : System.Web.UI.Page
    {
        public string msg, name = "Sign in";
        public string tablename = "tblUsers";
        Users_Controller controller_Users = new Users_Controller();
        public string password, username;
        int accesskey;
        string firstname;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                GETDATA();
                if (controller_Users.TestData(username, password))
                {
                    Session["userId"] = controller_Users.GetUserId(username);
                    Session["fusername"] = username;
                    Session["firstname"] = controller_Users.GetFirstName(username);
                    Session["accesskey"] = controller_Users.GetAccessKey(username);
                    Application.Lock();
                //    Application["users"] = (int)(Application["users"]) + 1;
                    Application.UnLock();
                    Response.Redirect("Landing_Page.aspx");
                    name = ($"Hello {username}");
                }
                else
                    msg = "Error, <br/> Your username or password is incorrect";
            }
        }
        void GETDATA()
        {
            password = Request.Form["pass"];
            username = Request.Form["usname"];
        }

    }
}