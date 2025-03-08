using ControllersProject.Controller;
using PlanDataWebService.Controller;
using PlanDataWebService.DBServer;
using System;
using System.Threading.Tasks;

namespace PlanDataWebService.View
{
    public partial class AddMuscles : System.Web.UI.Page
    {
        const string serverDB = "ServerDB.mdf"; // Adjust path as needed
        private DBOperations mcAsync = new DBOperations();
        private Muscles_Controller mc = new Muscles_Controller(serverDB);
        public string msg = "";
        private Analytics_Controller ac = new Analytics_Controller();
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submit"] != null)
            {
                string username = Session["username"].ToString();
                Controller_Users cu = new Controller_Users();
                int userId = cu.GetUserIdByUsername(username);
                string muscleName = Request.Form["muscle_name"];
                string muscleDesc = Request.Form["muscle_desc"];
                int muscleGroupId = int.Parse(Request.Form["muscle_group"]);
                
                bool result = mc.AddMuscle(muscleName, muscleDesc, muscleGroupId);

                if (result)
                {
                    msg = "Muscle added successfully!";
                    ac.AddInsert(userId);
                    try
                    {
                        await System.Threading.Tasks.Task.Run(() => mcAsync.AddNewMuscle(muscleName, muscleDesc, muscleGroupId));
                    }
                    catch(Exception exc)
                    {
                        Response.Write("Error");
                    }
                }
                else
                {
                  msg = "An error occured, please try again later";
                }
            }
        }
    }
}
