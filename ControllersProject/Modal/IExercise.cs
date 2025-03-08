using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public interface IExercise
    {
        int Id { get; set; }
        string Name { get; set; }
        string Instructions { get; set; }
        double Difficulty { get; set; }
        int Time_To_Complete { get; set; }
        LinkedList<int> MusclesWorked { get; set; }
        double Score { get; set; }
        Vector ExerciseVector { get; set; }

        void GetExerciseVector();
        string ToString();
    }
}
