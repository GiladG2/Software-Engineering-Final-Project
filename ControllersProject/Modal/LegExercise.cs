using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class LegExercise:Exercise
    {
        private double kneeDamage;//בין 0 ל1 , שומר את העומס של התרגיל על הברכיים
        private bool isSquatBased;//שומר האם התרגיל הוא חלק ממשפחת תרגילי הסקוואט
        //פעולה בונה עם פרמטרים
        public LegExercise(int id, string name, string instructions, double difficulty, int time_To_Complete,
            LinkedList<int> musclesWorked, double kneeDamage, bool isSquatBased) : base(
            id, name, instructions, difficulty, time_To_Complete, musclesWorked)
        {
            this.KneeDamage = kneeDamage;
            this.IsSquatBased = isSquatBased;
            GetExerciseVector();
        }
        //Properties to get and set private fields
        public double KneeDamage { get => kneeDamage; set => kneeDamage = value; }
        public bool IsSquatBased { get => isSquatBased; set => isSquatBased = value; }

        //Translates a leg exercise to a vector for calculations
        public new void GetExerciseVector()
        {
            double x = time_To_Complete;
            double y = difficulty;
            double z = IsSquatBased == true ? 5 * KneeDamage : 2 * KneeDamage;
            ExerciseVector = new Vector(x, y, z);
        }
    }
}
