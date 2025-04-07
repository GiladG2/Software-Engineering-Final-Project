using ControllersProject.Controller;
using ControllersProject.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrainingServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TrainingLogAPIController : ControllerBase
    {
        private Users_Controller _users_controller = new Users_Controller();
        private Plan_Controller _plan_controller = new Plan_Controller();
        [HttpGet("GetLoggedExercises")]
        public JsonResult GetLog(string username,string date)
        {
         int userId = _users_controller.GetUserId(username);
         int planId = _plan_controller.GetPlanIdByUser(userId);
         return new JsonResult(Ok(Training_Log_Controller.GetLogForAPI(planId,date)));
        }
        [HttpGet("GetMaxOrder")]
        public JsonResult GetMaxOrder(string username,string date)
        {
            int userId = _users_controller.GetUserId(username);
            int planId = _plan_controller.GetPlanIdByUser(userId);
            return new JsonResult(Ok(Training_Log_Controller.GetMaxOrder(planId, date)));
        }
        [HttpPost("LogExercise")]
        public JsonResult LogExercise(int exerciseId, string username, string date, int order, int reps, int weightKg)
        {
            int userId = _users_controller.GetUserId(username);
            int planId = _plan_controller.GetPlanIdByUser(userId);
            return new JsonResult(Ok(Training_Log_Controller.SaveExercise(planId,reps,weightKg,exerciseId,userId,date)));
        }
        [HttpDelete("DeleteLoggedExercise")]
        public JsonResult DeleteExercise(int exerciseId,int order,string username, string date) {

            int userId = _users_controller.GetUserId(username);
            int planId = _plan_controller.GetPlanIdByUser(userId);
            return new JsonResult(Ok(Training_Log_Controller.DeleteLoggedExercise(exerciseId,planId,date,order)));
        }
        [HttpPut("EditLoggedExercise")]
        public JsonResult EditExercise(int exerciseId, string username, string date, int order, int reps, double weightKg)
        {
            int userId = _users_controller.GetUserId(username);
            int planId = _plan_controller.GetPlanIdByUser(userId);
            return new JsonResult(Ok(Training_Log_Controller.SaveLogExerciseChangesAPI(exerciseId, planId, date, order, reps, weightKg)));
        }
        [HttpGet("GetGraphData")]
        public JsonResult GraphData(string username, int exerciseId,int period)
        {
            int userId = _users_controller.GetUserId(username);
            int planId = _plan_controller.GetPlanIdByUser(userId);
            return new JsonResult(Ok(Training_Log_Controller
            .GetGraphDataAPI(exerciseId,planId,period)));
        }

        [HttpGet("GetLogHistory")]

        public IActionResult GetLogHistory(string username)
        {
            int userId = _users_controller.GetUserId(username);
            return (Ok(Training_Log_Controller.GetLogGHistory(userId)));
        }
    }
}
