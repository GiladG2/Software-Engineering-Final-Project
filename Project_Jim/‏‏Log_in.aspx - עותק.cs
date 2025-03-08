using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Jim
{
    public partial class Log_in : System.Web.UI.Page
    {
        public string msg, name = "Sign in";
        public string tablename = "tblUsers";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                GETDATA();
                if (Testada())
                {
                    Session["fusername"] = username;
                    Session["firstname"] = firstname;
                    Session["accesskey"] = aceeskey;
                    Application.Lock();
                    Application["users"] = (int)(Application["users"])+1;
                    Application.UnLock();
                    Response.Redirect("Landing_Page1.aspx");
                    name = ($"Hello {username}");
                }
                else
                    msg = "Error, <br/> Your username or password is incorrect";
            }
        }
        public string password, username;
        int aceeskey;
        string firstname;
        void GETDATA()
        {
            password = Request.Form["pass"];
            username = Request.Form["usname"];
        }
        bool Testada()
        {
            string filename = "Database1.mdf";//שם מסד הנתונים
            string str = $"select * from tblUsers where fldUsername = '{username}' AND fldPassowrd= '{password}'";
            //לוקח את כל השורות שבהן שם המשתמש והסיסמה זהים למה שהמשתמש הכניס
            DataTable dt = AdoHelper.GetDataTable(filename, str);//הפעלת פעולה שמקבלת כמה שורות מקיימות את השאילתה שלמעלה
            if (dt.Rows.Count == 0)
                return false;
            //אם הוחזר 0, זה אומר שאין שורה כזאת מה שאומר שהמשתמש הכניס נתונים לא נכונים
            aceeskey = int.Parse((dt.Rows[0]["fldAccess"]).ToString());//לקיחת מפתח הגישה של המשתמש שמתחבר עכשיו
            firstname = (dt.Rows[0]["fldFirstbane"]).ToString();//לקיחת שם הפרטי של המתשמש שמתחבר עכשיו
            return true;

        }
    }
}