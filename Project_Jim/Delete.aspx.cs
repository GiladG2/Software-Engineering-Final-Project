using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Jim
{
    public partial class Delete : System.Web.UI.Page
    {
        public string tablename="tblUsers";//שם הטבלה
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] == null)//בדיקה האם הגולש מנסה למחוק משתמש שבכלל לא מחובר מה שיגרום לשגיאה
                Response.Redirect("Landing_Page1.aspx");//הוצאה החוצה מתהליך המחיקה אל דף הנחיתה
                Session.Abandon();//sessionניתוק ה 
                string username = Session["fusername"].ToString();//שנוצר כשהמשתמש התתחבר session לקיחת שם המשתמש מה
                string str = $"DELETE  from {tablename} where fldUsername='{username}'";//תמחק מהטבלה את המשתמש ששם המשתמש שלו הוא המשתמש שנלקח הרגע
                string fileName = "Database1.mdf";//שם מסד הנתונים
                int check= AdoHelper.CheckInsert(fileName, str);//הפעלת השאילתה
                Response.Redirect("Landing_Page1.aspx");//החזרה אחרי שהתשמש נמחק לדף הנחיתה
        }
    }
}