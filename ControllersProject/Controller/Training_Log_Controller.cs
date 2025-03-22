using ControllersProject.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Controller
{
    public class Training_Log_Controller
    {
        private static Modal_Training_Log tlm = new Modal_Training_Log();
        

        public static bool SaveExercise(int planId, int reps, int weight, int exerciseId, int userId, string date)
        {
            return Modal_Training_Log.SaveExercise(planId, reps, weight, exerciseId, userId, date);
        }
        public static int GetMaxOrder(int planId, string date)
        {
            return Modal_Training_Log.GetMaxOrder(planId, date) + 1;
        }
        public static string GetLog(string date, int plan_Id)
        {
            return tlm.GetLog(date, plan_Id);
        }
        public static List<TrainingLogExerciseFormat> GetLogForAPI(int planId,string date)
        {
            return tlm.GetLogForAPI(planId,date);
        }
        public static string GetGraphData(int exerciseId, int planId, int period = 0)
        {
            if (period == 0)
                return Modal_Training_Log.GetGraphData(exerciseId, planId);
            return Modal_Training_Log.GetGraphData(exerciseId, planId, period);
        }
        public static void SaveLogExerciseChanges(int exerciseId, int planId, string date, int order, int reps, double weightKg)
        {
            tlm.SaveLogChanges(exerciseId, planId, date, order, reps, weightKg);
        }
        public static bool SaveLogExerciseChangesAPI(int exerciseId, int planId, string date, int order, int reps, double weightKg)
        {
            return tlm.SaveLogChangesAPI(exerciseId, planId, date, order, reps, weightKg);
        }
        public static string DeleteLoggedExercise(int exerciseId, int planId, string date, int order)
        {
            return tlm.DeleteLoggedExercise(exerciseId, planId, date, order);
        }
        public static List<GraphDataFormat> GetGraphDataAPI(int exerciseId,int planId,int period)
        {
            return tlm.GetGraphDataAPI(exerciseId, planId, period);
        }
        public static Dictionary<string,Dictionary<string,object>> GetLogGHistory(int userId)
        {
            return tlm.GetLogInHistory(userId);
        }
    }
}
