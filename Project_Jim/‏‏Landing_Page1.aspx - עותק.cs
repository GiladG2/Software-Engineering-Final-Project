using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Jim
{
    public partial class Landing_Page1 : System.Web.UI.Page
    {
        
        public string firstname;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["firstname"] != null)
                firstname = (Session["firstname"]).ToString();

        }
    }
}
