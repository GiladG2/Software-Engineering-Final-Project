using System;
using System.Threading.Tasks;
using Project_Gym.Controller; // Adjust namespace as needed

namespace ExerciseFetcherConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var controller = new Controller_Exercises();
            await controller.FetchAndStoreExercisesAsync();
        }
    }
}
