using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class Exercise : IExercise
    {
        protected string name//Saves the exerise's name
       , instructions; //Saves the exercise's instructions
            
        protected int id,//Saves the exercise's id
            time_To_Complete;//Saves the amount of time it takes to do the exercise
        protected double difficulty;//Saves the exercise's difficulty
        protected LinkedList<int> musclesWorked;//Saves the muscles that are worked in the exercise
        protected int index;//Saves the exercise's order in a specific workout in a specific plan for a user

        //Properties to get the protected fields
        public int Id { get => id; set => id = value; }
        public double Difficulty { get => difficulty; set => difficulty = value; }
        public int Time_To_Complete { get => time_To_Complete; set => time_To_Complete = value; }
        public string Name { get => name; set => name = value; }
        public string Instructions { get => instructions; set => instructions = value; }
        public int Index { get => index; set => index = value; }
        public LinkedList<int> MusclesWorked { get => musclesWorked; set => musclesWorked = value; }
        public double Score { get => score; set => score = value; }
        public Vector ExerciseVector { get => exerciseVector; set => exerciseVector = value; }

        private Vector exerciseVector;
        private double score;
        // Constructor with parameters
        public Exercise(int id, string name, string instructions, double difficulty, int time_To_Complete, LinkedList<int> musclesWorked)
        {
            this.id = id;
            this.name = name;
            this.instructions = instructions;
            this.difficulty = difficulty;
            this.time_To_Complete = time_To_Complete;
            this.MusclesWorked = musclesWorked;
            this.index = 0;
        }
        //Translation of an exercise to a vector for calculations
        public void GetExerciseVector()
        {
            double x = time_To_Complete;//x = the time it takes to complete an exercise
            double y = difficulty;//y = the difficulty of an exercise
            double z = 0;//Set to zero, the z axis is used in the classes that inherit from this class (Exercise.cs)
            //the user vector
            ExerciseVector = new Vector(x, y, z);
        }
        //Returns the object's current state
        public override string ToString()
        {
            return $"{this.name}";
        }
    }
}

