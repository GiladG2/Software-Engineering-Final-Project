using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ControllersProject.Modal;
using ControllersProject.DAL;

namespace ControllersProject.Controller
{
    public class Exercise_Controller
    {
        private Modal_Exercises me = new Modal_Exercises();

        // Constructor overloads
        public Exercise_Controller(string dbName)
        {
            me = new Modal_Exercises(dbName);
        }

        public Exercise_Controller()
        {
            me = new Modal_Exercises();
        }

        // Synchronous methods
        public DataSet GetAllExercises()
        {
            return me.GetAllExercises();
        }
        public List<ExerciseFormat> GetExerciseList()
        {
            return me.GetExerciseList();
        }
        // Async method for GetAllExercises
        public async Task<DataSet> GetAllExercisesAsync()
        {
            return await me.GetAllExercisesAsync();  // Using async method from Modal
        }

        public bool AddExercise(string exerciseName, string exerciseDesc, double difficulty, int timeToComplete, int muscleId)
        {
            try
            {
                int nextExerciseId = me.GetNextExerciseId();
                if (me.AddExercise(exerciseName, exerciseDesc, difficulty, timeToComplete))
                {
                    return me.AddMuscleToExercise(nextExerciseId, muscleId);
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        // Async method for AddExercise
        public async Task<bool> AddExerciseAsync(string exerciseName, string exerciseDesc, double difficulty, int timeToComplete, int muscleId)
        {
            try
            {
                // Get the next exercise ID asynchronously
                int nextExerciseId = await me.GetNextExerciseIdAsync();

                // Add exercise asynchronously
                bool exerciseAdded = await me.AddExerciseAsync(exerciseName, exerciseDesc, difficulty, timeToComplete);

                if (exerciseAdded)
                {
                    // Add muscle to exercise asynchronously
                    return await me.AddMuscleToExerciseAsync(nextExerciseId, muscleId);
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool EditExercise(int exerciseId, string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            return me.EditExercise(exerciseId, exerciseName, exerciseDesc, difficulty, timeToComplete);
        }

        // Async method for EditExercise
        public async Task<bool> EditExerciseAsync(int exerciseId, string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            return await me.EditExerciseAsync(exerciseId, exerciseName, exerciseDesc, difficulty, timeToComplete);  // Using async method from Modal
        }

        public bool DeleteExercise(int exerciseId)
        {
            return me.DeleteExercise(exerciseId);
        }

        // Async method for DeleteExercise
        public async Task<bool> DeleteExerciseAsync(int exerciseId)
        {
            return await me.DeleteExerciseAsync(exerciseId);  // Using async method from Modal
        }

        public LinkedList<int> GetMusclesWorked(int exerciseId)
        {
            return me.GetMusclesWorked(exerciseId);
        }

        // Async method for GetMusclesWorked
        public async Task<LinkedList<int>> GetMusclesWorkedAsync(int exerciseId)
        {
            return await me.GetMusclesWorkedAsync(exerciseId);  // Using async method from Modal
        }

        public LinkedList<Exercise> AllExercises(string muscleName, Vector userVector)
        {
            return me.AllExercises(muscleName, userVector);
        }

        // Async method for AllExercises
        public async Task<LinkedList<Exercise>> AllExercisesAsync(string muscleName, Vector userVector)
        {
            return await me.AllExercisesAsync(muscleName, userVector);  // Using async method from Modal
        }

        public LinkedList<Exercise> SortExercisesByScore(LinkedList<Exercise> exercises)
        {
            return me.SortExercisesByScore(exercises);
        }

        // Async method for SortExercisesByScore
        public async Task<LinkedList<Exercise>> SortExercisesByScoreAsync(LinkedList<Exercise> exercises)
        {
            return await me.SortExercisesByScoreAsync(exercises);  // Using async method from Modal
        }

        public string GetExercisesList(int planId)
        {
            return me.GetExercisesList(planId);
        }

        public Exercise GetNextBestExercise(int userId, int exerciseId)
        {
            return me.GetNextBestExercise(userId, exerciseId);
        }

        // Async method for GetNextBestExercise
        public async Task<Exercise> GetNextBestExerciseAsync(int userId, int exerciseId)
        {
            return await me.GetNextBestExerciseAsync(userId, exerciseId);  // Using async method from Modal
        }

        public string GetExerciseName(int exerciseId)
        {
            return me.GetExerciseName(exerciseId);
        }

        // Async method for GetExerciseName
        public async Task<string> GetExerciseNameAsync(int exerciseId)
        {
            return await me.GetExerciseNameAsync(exerciseId);  // Using async method from Modal
        }

        // Async method for UpdateDb
     /*   public async Task<bool> UpdateDbAsync(string dbFilePath)
        {
            return await me.UpdateDbAsync(dbFilePath);  // Using async method from Modal
        }
     */
    }
}
