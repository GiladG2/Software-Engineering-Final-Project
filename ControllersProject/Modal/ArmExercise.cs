using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class ArmExercise:Exercise
    {
        private bool isolatesMuscle;
        private double wristStress; // 0-1 
        private int gripWidth; // 0 - narrow , 1- shoulder width, 2 - wide

        public ArmExercise(int id, string name, string instructions, double difficulty, int time_To_Complete, LinkedList<int> musclesWorked, int gripWidth, bool isolatesMuscle, double wristStress) : base(
           id, name, instructions, difficulty, time_To_Complete, musclesWorked)
        {
            this.IsolatesMuscle = isolatesMuscle;
            this.WristStress = wristStress;
            this.gripWidth = gripWidth;
            GetExerciseVector();
        }

        public int GripWidth { get => gripWidth; set => gripWidth = value; }
        public double WristStress { get => wristStress; set => wristStress = value; }
        public bool IsolatesMuscle { get => isolatesMuscle; set => isolatesMuscle = value; }

        public new void GetExerciseVector()
        {
            double x = time_To_Complete;
            double y = difficulty;
            double z = isolatesMuscle == true ? gripWidth * wristStress * 3 : gripWidth * wristStress;
            ExerciseVector = new Vector(x, y, z);
        }
    }
}
