using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using PlanDataWebService.Controller;
using PlanDataWebService.Modal;

namespace PlanDataWebService.View
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected int InsertCount { get; set; }
        protected int UpdateCount { get; set; }
        protected int DeleteCount { get; set; }
        public static int userId;
        static Analytics_Controller ac = new Analytics_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get username from session
            if (Session["username"] == null)
            Response.Redirect("Log_In.aspx");
            string username = Session["username"].ToString();
            // Fetch user ID using Controller_Users
            Controller_Users cu = new Controller_Users();
            userId = cu.GetUserIdByUsername(username);
            if (userId > 0)
            {
                // Fetch analytics data for the user
                var analytics = ac.GetAnalyticsForUser(userId);

                if (analytics != null)
                {
                    // Bind analytics data to properties
                    InsertCount = analytics.Inserts;
                    UpdateCount = analytics.Updates;
                    DeleteCount = analytics.Deletes;
                   
                }
            }
        }

        [WebMethod]
        public static string GetApiKey()
        { Controller_Users cu = new Controller_Users();
            return cu.GenerateAPIKey();
        }

        [WebMethod]
        public static string GetGraphData(int period, int operationId)
        {
            return ac.GetGraphData(userId,period,operationId);
        }
    }
}
