using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class Workout
    {
        private LinkedList<Exercise> exerciseList;//The workout's exercises
        private string name;//The workout's name (Pull,Legs,Push,Chest...)
        //Public property in order to access private fields
        public LinkedList<Exercise> ExerciseList { get => exerciseList; set => exerciseList = value; }
        public string Name { get => name; set => name = value; }
        //Constructors
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
