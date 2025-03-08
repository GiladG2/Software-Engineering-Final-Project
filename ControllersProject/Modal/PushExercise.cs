using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class PushExercise : Exercise
    {
        private double elbowDamage;//בין 0 ל1
        private double explosiveness;//בין 0 ל1

        public PushExercise(int id, string name, string instructions, double difficulty, int time_To_Complete, LinkedList<int> musclesWorked, double elbowDamage, double explosiveness) : base(
            id, name, instructions, difficulty, time_To_Complete, musclesWorked)
        {
            this.elbowDamage = elbowDamage;
            this.explosiveness = explosiveness;
            GetExerciseVector();
        }

        public double ElbowDamage { get => elbowDamage; set => elbowDamage = value; }
        public double Explosiveness { get => explosiveness; set => explosiveness = value; }

        public new void GetExerciseVector()
        {
            double x = time_To_Complete;
            double y = difficulty * (1 + Explosiveness);
            double z = ElbowDamage > 0.65 ? 5 * 4 * ElbowDamage : ElbowDamage;
            ExerciseVector = new Vector(x, y, z);
        }
    }
}
