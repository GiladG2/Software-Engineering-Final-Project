using ControllersProject.Controller;
using IronPdf.Engines.Marshalers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Gym.View
{
    public partial class Training_Log : System.Web.UI.Page
    {
        private Exercise_Controller ce = new Exercise_Controller();
        public string exercises = "";
        public Training_Log_Controller tl_Controller = new Training_Log_Controller();
        public int userId = 0;
        public int planId = 0;
        private static Plan_Controller plan_controller = new Plan_Controller();
        public static string log = "";
        public static DateTime currentDate = DateTime.Now.Date;
        public static string date = currentDate.ToString("yyyy-MM-dd");
        public Plan_Controller pc = new Plan_Controller();
        public static Progression_Controller progression_Controller = new Progression_Controller();
        public Users_Controller cu = new Users_Controller();
        static int user_Id;
        public double m = 0;
        public string feedback = "";
        public string msg = "";

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["fusername"] == null)
                Response.Redirect("Log_In.aspx");
            string username = Session["fusername"].ToString();
            userId = cu.GetUserId(username);
            if (!pc.HasAPlan(userId))
                Response.Redirect("Training_Plans.aspx");
            planId = pc.GetPlanIdByUser(userId);
            ce.GetExercisesList(planId);
              exercises = ce.GetExercisesList(planId);
            log = Training_Log_Controller.GetLog(date, planId);
            user_Id = userId;
            if (!IsPostBack)
            {
            feedback = progression_Controller.GetFeedbackForEntirePlan(planId);
            string creationDateStr = pc.GetPlanCreationDate(planId); 
            if (DateTime.TryParse(creationDateStr, out DateTime creationDate))
            {
                // Add three months to the creation date
                DateTime threeMonthsFromCreation = creationDate.AddMonths(3);

                // Compare with the current date
                if (threeMonthsFromCreation == DateTime.Now)
                {     
                        msg = await progression_Controller.GetFeedbackForPlanAsync(planId);
                    EmailSender.EmailSender email = new EmailSender.EmailSender();
                    email.SendPlanExpiringEmail(cu.GetEmail(username),cu.GetFirstName(username),msg,username);
                    msg = "<br/> An email with a detailed review about your progress so far has been sent!";
                }
            }
            }
            
        }
        [WebMethod]
        public static void SaveExercise(int planId, int reps,int weight, int exerciseId, int userId,string date)
        {
            Training_Log_Controller.SaveExercise(planId,reps,weight,exerciseId,userId,date);
        }
        [WebMethod]
        public static string GetLog(string date,int planId)
        {
           return Training_Log_Controller.GetLog(date,planId);
        }
        [WebMethod]
        public static string GetGraphData(int exerciseId, int planId ,int period)
        {
            return Training_Log_Controller.GetGraphData(exerciseId,planId,period);
        }
        [WebMethod]
        public static int GetPlanId()
        {
            return plan_controller.GetPlanIdByUser(user_Id);
        }
       
        [WebMethod]
        public static string GetProgressionReview(int planId,int period,int exerciseId)
        {
            return progression_Controller.TestGradeOfProgression(planId,period,exerciseId);
        }

        [WebMethod]
        public static int GetMaxOrder(int planId,string date)
        {
            return Training_Log_Controller.GetMaxOrder(planId,date);
        }
        [WebMethod]
        public static void SaveLogChanges(int exerciseId, int planId, string date, int order, int reps, double weightKg)
        {
            Training_Log_Controller.SaveLogExerciseChanges(exerciseId, planId, date, order, reps, weightKg);
        }
        [WebMethod]
        public static string DeleteLoggedExercise(int exerciseId, int planId, string date, int order)
        {
            return Training_Log_Controller.DeleteLoggedExercise(exerciseId, planId, date, order);
        }
        [WebMethod]
        public static string GetFeedbackForExercise(int userId,int planId,int period, int exerciseId)
        {
          return  progression_Controller.GetFeedbackForExercise(userId,planId,period,exerciseId);
        }
        [WebMethod]
        public static void UpdatePlan(int planId,int exerciseId, int newExerciseId)
        {
            plan_controller.UpdatePlan(planId,exerciseId,newExerciseId);
        }
        
    }
}