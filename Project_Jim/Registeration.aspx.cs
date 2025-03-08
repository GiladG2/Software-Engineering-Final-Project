using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Jim
{
    public partial class Dummy : System.Web.UI.Page
    {
        public string tablename = "tblUsers";
       public string password, username, firstname, phonenumber, date, cardio, boxing, run, climb,msg;
       public int gender=0,goal=0, access=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                GETDATA();
                if (Iftaken() == true)
                    msg = "this username is already taken";
                else
                {
                    if (Adddata() == true)
                    {if (Session["accesskey"] == null)
                            msg = "success!, <a href='Log_in.aspx'>back to Login</a>";
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
        bool Iftaken()
        {
            string fileName = "Database1.mdf";//שם מסד הנתונים
            string str = $"select * from tblUsers where fldusername='{username}'";//תיקח את כל המשתמשים עם השם שהמשתמש רוצה להכניס
            return AdoHelper.IsExist(fileName, str);//הפעולה פעולה הבודקת האם קיים כבר קיים כזה משתמש
        }
        bool Adddata()
        {
            string fileName = "Database1.mdf";//שם מסד הנתונים
            string sql = $"INSERT INTO tblUsers (fldUsername,fldpassowrd,fldFirstbane,fldPhone,fldGendetr,fldDate,fldGoals,fldAccess)";
            sql += $" VALUES('{username}','{password}','{firstname}','{phonenumber}','{gender}','{date}','{goal}','{access}')";
            //הכנסת הנתונים שהמשתמש הכניס לשורות המתאימות במסד הנתונים
            return AdoHelper.CheckInsert(fileName,sql)>0;//(הפעלת פעולה שבודקת כמה שורות הושפעו (אם המשתמש הוכנס בהצלחה למסד הנתונים
        }
    }
}