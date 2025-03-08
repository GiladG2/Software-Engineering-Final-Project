using ControllersProject.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Gym.View
{
    public partial class Edit_Profile : System.Web.UI.Page
    {
        public int goal = 0;
  
        public int gender=0;
        public string username = "";
        public string password = " ";
        public string msg = "";
        public string firstname = " ", phonenumber = " ", date = " " ,dateForInput="", cardio = " ", year, month, day, date1, msg2;
        public DataTable dt;
        public string tablename = "tblUsers";
        Users_Controller cu = new Users_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
       if (Session["fusername"] == null)
                Response.Redirect("Landing_Page.aspx");
            username = Session["fusername"].ToString();
            if (Request.Form["submit"] == null)
                GetdataFromDb();
        else
            {
                Getdata();
                cu.UpdateDataForEdit(username,password,date,phonenumber,firstname,gender,goal);
                Message();
            }
        }
        void GetdataFromDb()
        {
           
            password = cu.GetPassword(username);
            firstname = cu.GetFirstName(username);
            phonenumber = cu.GetPhonenumber(username);
            date = cu.GetDate(username);
            date = date.Replace(" 00:00:00", "");
            year = date.Substring(6, 4);
            month = date.Substring(3, 2);
            day = date.Substring(0, 2);
            date = $"{year}-{month}-{day}";
            if (cu.GetGenderName(username).Equals("male"))
                gender = 1;
            goal = cu.GetGoal(username);
        }

        void Getdata()
        {
            firstname = Request.Form["first"];
            password = Request.Form["pass"];
            phonenumber = Request.Form["phone"];
            date = Request.Form["date"];
            if (Request.Form["gender"] == "male")
                gender = 1;
            else
                gender = 0;
            if (Request.Form["goal"] == "lose")
                goal = 1;

        }
        void Message()
        {
            //string password, string date, string phonenumber, string firstname,string gender,int goal, string username
            if (cu.UpdateDataForEdit(username,password,date,phonenumber,firstname,gender,goal))//הפעלת פעולה שבודקת האם שורות השתנו במסד התונים 0-לא השתנה כלום זאת אומרת שאם הכל עבר בהצלחה אמור להופיע מעל 0
                msg = "Your profile has been updated";//שינוי ההודעה להודעה מתאימה
            else
            {
                msg = "The values have not been updated";//שינוי ההודעה להודעה מתאימה
                msg2 = "please check again if the data that you entered is valid";//שינוי ההודעה להודעה מתאימה
            }
                
        }
    }
}