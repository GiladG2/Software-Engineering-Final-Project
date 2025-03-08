using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class Workout
    {
        private LinkedList<Exercise> exerciseList;
        private string name;
        public LinkedList<Exercise> ExerciseList { get => exerciseList; set => exerciseList = value; }
        public string Name { get => name; set => name = value; }
        public Workout(string name)
        {
            exerciseList = new LinkedList<Exercise>();
            this.name = name;
        }
        public Workout()
        {

        }
    }
}
