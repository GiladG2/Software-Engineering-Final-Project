using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class LegExercise:Exercise
    {
        private double kneeDamage;//בין 0 ל1
        private bool isSquatBased;

        public LegExercise(int id, string name, string instructions, double difficulty, int time_To_Complete, LinkedList<int> musclesWorked, double kneeDamage, bool isSquatBased) : base(
            id, name, instructions, difficulty, time_To_Complete, musclesWorked)
        {
            this.KneeDamage = kneeDamage;
            this.IsSquatBased = isSquatBased;
            GetExerciseVector();
        }

        public double KneeDamage { get => kneeDamage; set => kneeDamage = value; }
        public bool IsSquatBased { get => isSquatBased; set => isSquatBased = value; }

        public new void GetExerciseVector()
        {
            double x = time_To_Complete;
            double y = difficulty;
            double z = IsSquatBased == true ? 5 * KneeDamage : 2 * KneeDamage;
            ExerciseVector = new Vector(x, y, z);
        }
    }
}
