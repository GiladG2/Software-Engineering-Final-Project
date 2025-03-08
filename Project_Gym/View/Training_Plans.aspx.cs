using ControllersProject.Controller;
using ControllersProject.Modal;
using Project_Gym.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Gym.View
{
    public partial class Training_Plans : System.Web.UI.Page
    {
        public int duration, daysAWeek, experience, typeOfTraining, user_id;
        public string msg = "",link = "", downloadName="",pdfLink = "", pdfDownloadName = "";
        private LinkedList<string> pdfResponse;
        public int[] injuryList;
        public bool filedARequest,hasAplan;
        public string response="";
        private User_Requests_Controller cur = new User_Requests_Controller();
        public Plan_Controller plan_c = new Plan_Controller();
        LinkedList<Workout> plan = new LinkedList<Workout>();
        private Users_Controller cu = new Users_Controller();
        private PDF_Controller pdfControllder = new PDF_Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] == null)
                Response.Redirect("Landing_Page.aspx");
            user_id = int.Parse(Session["userId"].ToString());
            filedARequest = plan_c.UserFiledARequest(user_id);
            hasAplan = plan_c.HasAPlan(user_id);
            Plan_Controller plan_controller = new Plan_Controller();
            if (!hasAplan )
            {
                if (Session["plan"] == null)
                    GeneratePlan();
            }
            else
            {
            pdfResponse = DisplayPlan();        
            msg = pdfControllder.CreatePdfFile(Session["fusername"].ToString(),pdfResponse);
            }
                

            downloadName = $"{Session["fusername"].ToString()}'s Plan";
            pdfLink = $"/Downloads/PDF/{Session["fusername"].ToString()}'s plan.pdf";              
            if (!filedARequest && IsPostBack)
            {
                GetDataFromForm();
                Session["plan"] = null;

                if (Request.Form["submit"] != null)
                {
                        if (cur.AddUserRequest(user_id, duration, daysAWeek, experience, typeOfTraining, injuryList))
                    {                   
                        msg = "Request added successfully";
                        Response.Redirect(Request.RawUrl); // Reload the page to refresh the request status

                    }
                    else
                            msg = "Error, please try again later";
                }         
            }
        }
            public void InsertPlan(object sender, EventArgs e)
            {
            plan = (LinkedList<Workout>)Session["plan"];
                plan_c.InsertPlan(plan, user_id);
               Response.Redirect(Request.RawUrl);
        }
        void GetDataFromForm()
        {
                duration = int.Parse(Request.Form["duration"]);

                daysAWeek = int.Parse(Request.Form["days"]);

                experience = int.Parse(Request.Form["exp"]);

                typeOfTraining = int.Parse(Request.Form["typeOfPlan"]);
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
        } 
        public void ReGeneratePlan(object sender, EventArgs e)
        {
            Session["plan"] = null;
            /*LinkedList<Workout> plan = (LinkedList<Workout>)Session["plan"];
            List<Workout> planIteration = plan.ToList();
            for(int i =0;i<planIteration.Count;i++)
            {
                if (planIteration[i].ExerciseList.Count == 0 || planIteration[i].ExerciseList.Count == )
                    planIteration[i] = plan_c.GenerateWorkoutByName(planIteration[i].Name);
                else
                    planIteration[i].ExerciseList.RemoveFirst();
            }
            
            Session["plan"] = plan;*/
            Response.Redirect(Request.RawUrl);
        }
        void GeneratePlan()
        {
            plan = plan_c.CreateAPlan(user_id);
            response = "<div>";
            foreach (Workout workout in plan)
            {
                response += "<div class='workout'>";
                response += $"<h2>{workout.Name}</h2>";
                foreach (Exercise exercise in workout.ExerciseList)
                {
                    response += $"{exercise.ToString()}";
                    response += $"<br/>";
                }
                response += "</div>";
                response += "<br/>";
            }
            response += "</div>";
            Session["plan"] = plan;
        }
        LinkedList<string> DisplayPlan()
        {
            plan = plan_c.GetPlan(user_id);
            response = "<div>";
            LinkedList<string> pdfResponse = new LinkedList<string>();
            foreach (Workout workout in plan)
            {
                response += "<div class='workout'>";
                response += $"<h2>{workout.Name}</h2> - ";

                pdfResponse.AddLast($"{workout.Name}");
                foreach (Exercise exercise in workout.ExerciseList)
                {
                    response += $"{exercise.ToString()}";
                    response += $"<br/>";
                    pdfResponse.AddLast($"{exercise.ToString()}");
                }
                response += "</div>";
                response += "<br/>";
                pdfResponse.AddLast("");
            }
            response += "</div>";
            return pdfResponse;
           
        }


        protected void ReplaceWorkout_Command(object sender, CommandEventArgs e)
        {
            // Get the workout name (or ID) from CommandArgument
            string workoutName = e.CommandArgument.ToString();

            // Logic to replace the workout
            ReplaceWorkout(workoutName);
        }

        private void ReplaceWorkout(string workoutName)
        {
            Response.Redirect("Landing_Page.aspx");
        }
       
    }
}
