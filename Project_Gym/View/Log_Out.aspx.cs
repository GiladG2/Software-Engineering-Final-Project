using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Gym.View
{
    public partial class Log_Out : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();//session סגירת ה 
            Application.Lock();//נעילת אפליקישן שסוכם כמה אנשים מחוברים כרגע
          //  Application["users"] = (int)Application["users"] - 1;//הורדת משתמש עקב הניתוק
            Application.UnLock();//שיחרור האפליקישן
            Response.Redirect("Landing_Page.aspx");//החזרה לדף הנחיתה
        }
    }
}