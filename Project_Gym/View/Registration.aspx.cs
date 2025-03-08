using ControllersProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Gym.View
{
    public partial class Registration : System.Web.UI.Page
    {
        public string tablename = "tblUsers";
        public string password, username, firstname, phonenumber, date, cardio, boxing, run, climb, msg,email;
        public int gender = 0, goal = 0, access = 0;
        Users_Controller controller_Users = new Users_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                GETDATA();
                if (controller_Users.IsTaken(username) == true)
                    msg = "this username is already taken";
                else
                {
                    if (controller_Users.AddData(username,password,firstname,phonenumber,gender,date,goal,access, email) == true)
                    {
                        if (Session["accesskey"] == null)
                        {
                            msg = "success!, <a href='Log_in.aspx'>back to Login</a>";
                            try
                            {
                                EmailSender.EmailSender emailSender = new EmailSender.EmailSender();
                                emailSender.SendWelcomeEmail(email, firstname, username, password);
                            }
                            catch
                            {

                            }
                        }
                        else
                            Response.Redirect("Admin.aspx");
                    }
                    else
                        msg = "please try again later";
                }

            }
        }
        void GETDATA()
        {
            firstname = Request.Form["first"];
            password = Request.Form["pass"];
            username = Request.Form["gainus"];
            email = Request.Form["email"];
            phonenumber = Request.Form["phone"];
            date = Request.Form["date"];
            cardio = Request.Form["select"];
            if (Request.Form["gender"] == "male")
                gender = 1;
            if (Request.Form["goal"] == "lose")
                goal = 1;
            if (Request.Form["access"] == "7")
                access = 7;
            ;
        }
        

    }
}