using System.Data;
using System.Threading.Tasks;
using ControllersProject.Modal;

namespace ControllersProject.Controller
{
    public class Muscles_Controller
    {
        private Modal_Muscles mm;

        // Constructor with dbName
        public Muscles_Controller(string dbName)
        {
            mm = new Modal_Muscles(dbName);
        }

        // Default constructor
        public Muscles_Controller()
        {
            mm = new Modal_Muscles();
        }

        // Synchronous method to get all muscles
        public DataSet GetMuscles()
        {
            return mm.GetAllMuscles();
        }

        // Async method for GetMuscles
        public async Task<DataSet> GetMusclesAsync()
        {
            return await Task.Run(() => mm.GetAllMuscles());
        }

        // Synchronous method to add a muscle
        public bool AddMuscle(string muscleName, string muscleDescription, int muscleGroupId)
        {
            return mm.AddMuscle(muscleName, muscleDescription, muscleGroupId);
        }

        // Async method for AddMuscle
        public async Task<bool> AddMuscleAsync(string muscleName, string muscleDescription, int muscleGroupId)
        {
            return await Task.Run(() => AddMuscle(muscleName, muscleDescription, muscleGroupId));
        }

        // Synchronous method to edit a muscle
        public bool EditMuscle(int muscleId, string muscleName)
        {
            return mm.EditMuscle(muscleId, muscleName);
        }

        // Async method for EditMuscle
        public async Task<bool> EditMuscleAsync(int muscleId, string muscleName)
        {
            return await Task.Run(() => EditMuscle(muscleId, muscleName));
        }

        // Synchronous method to delete a muscle
        public bool DeleteMuscle(int muscleId)
        {
            return mm.DeleteMuscle(muscleId);
        }

        // Async method for DeleteMuscle
        public async Task<bool> DeleteMuscleAsync(int muscleId)
        {
            return await Task.Run(() => DeleteMuscle(muscleId));
        }

        // Synchronous method to get muscles for dropdown
        public string GetMusclesForDropdown()
        {
            return mm.GetAllMusclesForDropdown();
        }

        // Async method for GetMusclesForDropdown
        public async Task<string> GetMusclesForDropdownAsync()
        {
            return await Task.Run(() => mm.GetAllMusclesForDropdown());
        }

        // Synchronous method to get muscle name from ID
        public string GetMuscleNameFromId(int muscleId)
        {
            return mm.GetMuscleNameFromId(muscleId);
        }

        // Async method for GetMuscleNameFromId
        public async Task<string> GetMuscleNameFromIdAsync(int muscleId)
        {
            return await Task.Run(() => GetMuscleNameFromId(muscleId));
        }
    }
}
