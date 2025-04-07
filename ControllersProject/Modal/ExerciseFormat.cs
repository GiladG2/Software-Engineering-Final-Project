using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class ExerciseFormat
    {
        private int value;//Saves the Exercise's Id
        private string label;//Saves the Exercise's Name
        public ExerciseFormat(int value, string label)
        {
            this.value = value;
            this.label = label;
        }

        public int Value { get => value; set => this.value = value; }
        public string Label { get => label; set => label = value; }
    }
}
   
