using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class PullExercise:Exercise
    {
        private bool pullAngle; // horizontal -false, true - vertical
        private bool isAssisted;
        private int handPosition;// 0-neutral, 1- overhand, 2 - underhand
        public PullExercise(int id, string name, string instructions, double difficulty, int time_To_Complete, LinkedList<int> musclesWorked, bool pullAngle, bool isAssisted, int handPosition) : base(
           id, name, instructions, difficulty, time_To_Complete, musclesWorked)
        {
            this.IsAssisted = isAssisted;
            this.HandPosition = handPosition;
            this.PullAngle = pullAngle;
            GetExerciseVector();
        }

        public bool PullAngle { get => pullAngle; set => pullAngle = value; }
        public bool IsAssisted { get => isAssisted; set => isAssisted = value; }
        public int HandPosition { get => handPosition; set => handPosition = value; }

        public new void GetExerciseVector()
        {
            double x = time_To_Complete;
            double y = difficulty;
            double z = isAssisted == false ? handPosition * 1.5 + 1 : handPosition;
            ExerciseVector = new Vector(x, y, z);
        }
    }
}
