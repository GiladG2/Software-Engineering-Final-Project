using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Jim
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public int nu;
        public string str="";   

        protected void Page_Load(object sender, EventArgs e)
        {  if (Application["users"] == null)
                Application["users"] = 0;
            nu = (int)Application["users"];
           

        }
    }
}