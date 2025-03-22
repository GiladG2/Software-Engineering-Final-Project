using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class TrainingLogExerciseFormat
    {
        private string exerciseName;
        private int exerciseId;
        private int order;
        private int reps;
        private double weight;
        public TrainingLogExerciseFormat(string exerciseName, int exerciseId, int order, int reps, double weight)
        {
            this.exerciseName = exerciseName;
            this.exerciseId = exerciseId;
            this.order = order;
            this.reps = reps;
            this.weight = weight;
        }

        public string ExerciseName { get => exerciseName; set => exerciseName = value; }
        public int ExerciseId { get => exerciseId; set => exerciseId = value; }
        public int Order { get => order; set => order = value; }
        public int Reps { get => reps; set => reps = value; }
        public double Weight { get => weight; set => weight = value; }
    }
}
