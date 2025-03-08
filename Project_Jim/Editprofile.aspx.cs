using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Project_Jim
{
    public partial class Editprofile : System.Web.UI.Page
    {   public int  goal = 0;
        public int gender=0;
        public string username="";
        public string password=" ";
        public string msg = "";
        public string firstname = " ", phonenumber = " ", date = " ", cardio = " ", year, month, day, date1, msg2;
        DataTable dt;
        public string tablename="tblUsers";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] == null)
                Response.Redirect("Landing_Page1.aspx");
            username = Session["fusername"].ToString();
            if(Request.Form["submit"] == null)
            {
                GetdataFromDb();
            }
            else
            {
               Getdada();
               Updatedata();
            }
        }
        void GetdataFromDb()
        {
            string file = "Database1.mdf";
            string str = $"select * from {tablename} where fldusername='{username}'";
            dt = AdoHelper.GetDataTable(file,str);
            password = dt.Rows[0]["fldPassowrd"].ToString();
            firstname = dt.Rows[0]["fldFirstbane"].ToString();
            phonenumber = dt.Rows[0]["fldPhone"].ToString();
            date = dt.Rows[0]["fldDate"].ToString();
            date = date.Replace(" 00:00:00", "");
            year = date.Substring(6,4);
            month = date.Substring(3, 2);
            day = date.Substring(0, 2);
            date =$"{year}-{month}-{day}";
            if (dt.Rows[0]["fldGendetr"].ToString() == "True")
                gender = 1;
            if (dt.Rows[0]["fldGoals"].ToString() == "True")
                goal = 1;
        }
       
        void Getdada()
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
        void Updatedata()
        {
            string file = "Database1.mdf";//שם מסד הנתונים
            string str = $"UPDATE {tablename} SET fldPassowrd = '{password}', fldDate='{date}', fldPhone='{phonenumber}',fldFirstbane='{firstname}', fldGendetr='{gender}',fldGoals='{goal}' WHERE fldUsername = '{username}' ";
            //הכנסת הנתונים החדשים לשורות המתאימות במד הנתונים
            if (AdoHelper.CheckInsert(file, str) > 0)//הפעלת פעולה שבודקת האם שורות השתנו במסד התונים 0-לא השתנה כלום זאת אומרת שאם הכל עבר בהצלחה אמור להופיע מעל 0
                msg = "The values have been updated";//שינוי ההודעה להודעה מתאימה
            else
            {
                msg = "The values have not been updated";//שינוי ההודעה להודעה מתאימה
                msg2 = "please check again if the data that you entered is valid";//שינוי ההודעה להודעה מתאימה
            }
        }
    }
}