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
    public partial class GeneralMasterPage : System.Web.UI.MasterPage
    {
        public int nu;
        public string str = "";
        public string planMsg = "Get a plan";
       private Plan_Controller plan_controller = new Plan_Controller();
        public bool hasAplan = false;
        public string unseenMessages = "";
        private Xml_Controller xml_controller = new Xml_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["users"] == null)
                Application["users"] = 0;
            nu = (int)Application["users"];
            if (Session["userId"] != null)
            {
                unseenMessages = (xml_controller.GetUnseenResponses(Session["fusername"].ToString()));
                if (plan_controller.HasAPlan(int.Parse(Session["userId"].ToString())))
                {
                    planMsg = "Your plan";
                    hasAplan = true;
                }
            }
        }
        
    }
}