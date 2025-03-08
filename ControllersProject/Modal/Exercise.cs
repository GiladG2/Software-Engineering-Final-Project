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
        protected string name, instructions;
        protected int id, time_To_Complete;
        protected double difficulty;
        protected LinkedList<int> musclesWorked;//לעבור לLIST
        protected int index;
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
        public void GetExerciseVector()
        {
            double x = time_To_Complete;
            double y = difficulty;
            double z = 0;
            ExerciseVector = new Vector(x, y, z);
        }
        public override string ToString()
        {
            return $"{this.name}";
        }
    }
}

