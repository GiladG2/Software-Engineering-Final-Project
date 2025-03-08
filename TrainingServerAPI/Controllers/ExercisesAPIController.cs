using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrainingServerAPI.Modals;
using ControllersProject.Controller;
using System.Collections.Generic;
using System.Data;
using ControllersProject.Modal;

namespace TrainingServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesAPIController : ControllerBase
    {
        private readonly Exercise_Controller _exerciseController;

        public ExercisesAPIController()
        {
            _exerciseController = new Exercise_Controller(); // Default constructor, if you have a database name, pass it here
        }
        
        // Get all exercises (synchronous)
        [HttpGet("all")]
        public IActionResult GetAllExercises()
        {
            var exercises = _exerciseController.GetAllExercises();
            return Ok(exercises);
        }

        // Get all exercises (asynchronous)
        [HttpGet("all-async")]
        public async Task<IActionResult> GetAllExercisesAsync()
        {
            var exercises = await _exerciseController.GetAllExercisesAsync();
            return Ok(exercises);
        }
        [HttpGet("GetExerciseList")]
        public JsonResult GetExercises()
        {
            return new JsonResult(Ok(_exerciseController.GetExerciseList()));
        }
        // Add new exercise (synchronous)
        [HttpPost("add")]
        public IActionResult AddExercise([FromBody] Exercise newExercise)
        {
            bool result = _exerciseController.AddExercise(newExercise.Name, newExercise.Instructions, newExercise.Difficulty, newExercise.Time_To_Complete, newExercise.MusclesWorked.First());
            if (result)
                return Ok("Exercise added successfully.");
            return BadRequest("Failed to add exercise.");
        }

        // Add new exercise (asynchronous)
        [HttpPost("add-async")]
        public async Task<IActionResult> AddExerciseAsync([FromBody] Exercise newExercise)
        {
            bool result = await _exerciseController.AddExerciseAsync(newExercise.Name, newExercise.Instructions, newExercise.Difficulty, newExercise.Time_To_Complete, newExercise.MusclesWorked.First());
            if (result)
                return Ok("Exercise added successfully.");
            return BadRequest("Failed to add exercise.");
        }

        // Delete exercise (synchronous)
        [HttpDelete("delete/{exerciseId}")]
        public IActionResult DeleteExercise(int exerciseId)
        {
            bool result = _exerciseController.DeleteExercise(exerciseId);
            if (result)
                return Ok("Exercise deleted successfully.");
            return BadRequest("Failed to delete exercise.");
        }

        // Delete exercise (asynchronous)
        [HttpDelete("delete-async/{exerciseId}")]
        public async Task<IActionResult> DeleteExerciseAsync(int exerciseId)
        {
            bool result = await _exerciseController.DeleteExerciseAsync(exerciseId);
            if (result)
                return Ok("Exercise deleted successfully.");
            return BadRequest("Failed to delete exercise.");
        }

        // Get muscles worked by a specific exercise (synchronous)
        [HttpGet("muscles/{exerciseId}")]
        public IActionResult GetMusclesWorked(int exerciseId)
        {
            var muscles = _exerciseController.GetMusclesWorked(exerciseId);
            return Ok(muscles);
        }

        // Get muscles worked by a specific exercise (asynchronous)
        [HttpGet("muscles-async/{exerciseId}")]
        public async Task<IActionResult> GetMusclesWorkedAsync(int exerciseId)
        {
            var muscles = await _exerciseController.GetMusclesWorkedAsync(exerciseId);
            return Ok(muscles);
        }

        // Get next best exercise based on user (synchronous)
        [HttpGet("next-best/{userId}/{exerciseId}")]
        public IActionResult GetNextBestExercise(int userId, int exerciseId)
        {
            var nextExercise = _exerciseController.GetNextBestExercise(userId, exerciseId);
            return Ok(nextExercise);
        }

        // Get next best exercise based on user (asynchronous)
        [HttpGet("next-best-async/{userId}/{exerciseId}")]
        public async Task<IActionResult> GetNextBestExerciseAsync(int userId, int exerciseId)
        {
            var nextExercise = await _exerciseController.GetNextBestExerciseAsync(userId, exerciseId);
            return Ok(nextExercise);
        }

        // Get exercise name (synchronous)
        [HttpGet("name/{exerciseId}")]
        public IActionResult GetExerciseName(int exerciseId)
        {
            var exerciseName = _exerciseController.GetExerciseName(exerciseId);
            return Ok(exerciseName);
        }

        // Get exercise name (asynchronous)
        [HttpGet("name-async/{exerciseId}")]
        public async Task<IActionResult> GetExerciseNameAsync(int exerciseId)
        {
            var exerciseName = await _exerciseController.GetExerciseNameAsync(exerciseId);
            return Ok(exerciseName);
        }
    }
}
