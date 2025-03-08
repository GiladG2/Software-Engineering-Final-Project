using ControllersProject.Controller;
using System;
using System.Web;
using System.Web.UI;

namespace Project_Gym.View
{
    public partial class EditRequests : System.Web.UI.Page
    {
        public string msg = "";
        public int user_Id, workoutDuration, typeOfTraining, experience, daysAWeek;
        int[] injuryList;
        private User_Requests_Controller controllerUserRequests = new User_Requests_Controller();
        private Plan_Controller plan_Controller = new Plan_Controller();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] == null)
                Response.Redirect("Landing_Page.aspx");

            user_Id = int.Parse(Session["userId"].ToString());

            if (Request.Form["submit"] == null)
            {
                GetUserData();
                PopulateFields();
            }
            else
            {
                Submit_Click(sender, e);
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            workoutDuration = int.Parse(duration.Value);
            typeOfTraining = int.Parse(typeOfPlan.Value);
            daysAWeek = int.Parse(days.Value);
            experience = int.Parse(exp.Value);

            string[] selectedInjuries = Request.Form.GetValues("injuries[]");

            if (selectedInjuries != null)
            {
                injuryList = new int[selectedInjuries.Length];
                for (int i = 0; i < selectedInjuries.Length; i++)
                {
                    if (int.TryParse(selectedInjuries[i], out int injuryValue))
                    {
                        injuryList[i] = injuryValue;
                    }
                    else
                    {
                        injuryList[i] = -1;
                    }
                }
            }
            else
            {
                injuryList = new int[0];
            }
           
            if(!controllerUserRequests.EditUserRequest(user_Id, workoutDuration, daysAWeek, experience, typeOfTraining, injuryList))
            {
                msg = "Error, please try again later";
                return;
            }

            plan_Controller.DeletePlan(user_Id);
            Session["plan"] = null;
            Response.Redirect("Training_Plans.aspx");
        }

        private void GetUserData()
        {
            workoutDuration = controllerUserRequests.GetDuration(user_Id);
            typeOfTraining = controllerUserRequests.GetTypeOfTraining(user_Id);
            experience = controllerUserRequests.GetExperience(user_Id);
            daysAWeek = controllerUserRequests.GetDaysAWeek(user_Id);
        }

        private void PopulateFields()
        {
            // Populate form fields with user data
            duration.Value = workoutDuration.ToString();
            exp.Value = experience.ToString();
            typeOfPlan.Value = typeOfTraining.ToString();
            days.Value = daysAWeek.ToString();
        }
    }
}
