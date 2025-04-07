using ControllersProject.Controller;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Web.Services;

namespace WebServiceSoapAPI
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class DBOperations : System.Web.Services.WebService
    {
        public Exercise_Controller exerciseAPIController = new Exercise_Controller("ServerDB.mdf");
        public Exercise_Controller exerciseController = new Exercise_Controller();
        public Muscles_Controller musclesAPController = new Muscles_Controller("ServerDB.mdf");
        public Muscles_Controller musclesController = new Muscles_Controller();
        // *** ADD NEW MUSCLE ***
        [WebMethod]
        public bool AddNewMuscleAPI(string muscleName, string description, int muscleGroup)
        {
            return !string.IsNullOrWhiteSpace(muscleName) && !string.IsNullOrWhiteSpace(description) && muscleGroup >= 0 &&
                   musclesAPController.AddMuscle(muscleName, description, muscleGroup);
        }
        //הוספת שריר
        [WebMethod]
        public bool AddNewMuscle(string muscleName, string description, int muscleGroup)
        {
            return !string.IsNullOrWhiteSpace(muscleName) && !string.IsNullOrWhiteSpace(description) && muscleGroup >= 0 &&
                musclesController.AddMuscle(muscleName, description, muscleGroup);
        }

        // *** DELETE MUSCLE ***
        [WebMethod]
        public bool DeleteMuscleAPI(int muscleId)
        {
            return muscleId >= 0 && musclesAPController.DeleteMuscle(muscleId);
        }
        //מחיקת שריר
        [WebMethod]
        public bool DeleteMuscle(int muscleId)
        {
            return muscleId >= 0 && musclesController.DeleteMuscle(muscleId);
        }

        // *** ADD NEW EXERCISE ***
        [WebMethod]
        public bool AddNewExerciseAPI(string exerciseName, string exerciseDesc, double difficulty, int timeToComplete, int muscleId)
        {
            return !string.IsNullOrWhiteSpace(exerciseName) && !string.IsNullOrWhiteSpace(exerciseDesc) && difficulty >= 0 && timeToComplete >= 0 && muscleId >= 0 &&
                   exerciseAPIController.AddExercise(exerciseName, exerciseDesc, difficulty, timeToComplete, muscleId);
        }


        //הוספת תרגיל
        [WebMethod]
        public bool AddNewExercise(string exerciseName, string exerciseDesc, double difficulty, int timeToComplete, int muscleId)
        {
            return !string.IsNullOrWhiteSpace(exerciseName) && 
                !string.IsNullOrWhiteSpace(exerciseDesc) && difficulty >= 0 && timeToComplete >= 0 && muscleId >= 0 &&
                   exerciseController.AddExercise(exerciseName, exerciseDesc, difficulty, timeToComplete, muscleId);
        }

        // *** DELETE EXERCISE ***
        [WebMethod]
        public bool DeleteExerciseAPI(int exerciseId)
        {
            return exerciseId >= 0 && exerciseAPIController.DeleteExercise(exerciseId);
        }

        [WebMethod]
        public bool DeleteExercise(int exerciseId)
        {
            return exerciseId >= 0 && exerciseController.DeleteExercise(exerciseId);
        }

        // *** GET ALL EXERCISES ***
        [WebMethod]
        public DataSet GetAllExercisesAPI()
        {
            return exerciseAPIController.GetAllExercises();
        }

        [WebMethod]
        public DataSet GetAllExercises()
        {
            return exerciseController.GetAllExercises();
        }

        // *** EDIT EXERCISE - Async API Controller ***
        [WebMethod]
        public async Task<bool> EditExerciseAPIAsync(int exerciseId, string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            return exerciseId >= 0 && !string.IsNullOrWhiteSpace(exerciseName) && !string.IsNullOrWhiteSpace(exerciseDesc) && difficulty >= 0 && timeToComplete >= 0 &&
                   await exerciseAPIController.EditExerciseAsync(exerciseId, exerciseName, exerciseDesc, difficulty, timeToComplete);
        }

        // *** EDIT EXERCISE - Sync API Controller ***
        [WebMethod]
        public bool EditExerciseAPI(int exerciseId, string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            return exerciseId >= 0 && !string.IsNullOrWhiteSpace(exerciseName) && !string.IsNullOrWhiteSpace(exerciseDesc) && difficulty >= 0 && timeToComplete >= 0 &&
                   exerciseAPIController.EditExerciseAsync(exerciseId, exerciseName, exerciseDesc, difficulty, timeToComplete).Result;
        }

        // *** EDIT EXERCISE - Async Normal Controller ***
        [WebMethod]
        public async Task<bool> EditExerciseAsync(int exerciseId, string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            return exerciseId >= 0 && !string.IsNullOrWhiteSpace(exerciseName) && !string.IsNullOrWhiteSpace(exerciseDesc) && difficulty >= 0 && timeToComplete >= 0 &&
                   await exerciseController.EditExerciseAsync(exerciseId, exerciseName, exerciseDesc, difficulty, timeToComplete);
        }

        // *** EDIT EXERCISE - Sync Normal Controller ***
        [WebMethod]
        public bool EditExercise(int exerciseId, string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            return exerciseController.EditExercise(exerciseId, exerciseName, exerciseDesc, difficulty, timeToComplete);
        }

        // *** EDIT MUSCLE - Async API Controller ***
        [WebMethod]
        public async Task<bool> EditMuscleAPIAsync(int muscleId, string muscleName)
        {
            return muscleId >= 0 && !string.IsNullOrWhiteSpace(muscleName) &&
                   await musclesAPController.EditMuscleAsync(muscleId, muscleName);
        }

        // *** EDIT MUSCLE - Sync API Controller ***
        [WebMethod]
        public bool EditMuscleAPI(int muscleId, string muscleName)
        {
            return muscleId >= 0 && !string.IsNullOrWhiteSpace(muscleName) &&
                   musclesAPController.EditMuscleAsync(muscleId, muscleName).Result;
        }

        // *** EDIT MUSCLE - Async Normal Controller ***
        [WebMethod]
        public async Task<bool> EditMuscleAsync(int muscleId, string muscleName)
        {
            return muscleId >= 0 && !string.IsNullOrWhiteSpace(muscleName) &&
                   await musclesController.EditMuscleAsync(muscleId, muscleName);
        }

        // *** EDIT MUSCLE - Sync Normal Controller ***
        [WebMethod]
        public bool EditMuscle(int muscleId, string muscleName)
        {
            return muscleId >= 0 && !string.IsNullOrWhiteSpace(muscleName) &&
                   musclesController.EditMuscle(muscleId, muscleName);
        }
    }
}
