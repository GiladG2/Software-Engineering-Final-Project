using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class ExerciseFormat
    {
        private int value;
        private string label;
        public ExerciseFormat(int value, string label)
        {
            this.value = value;
            this.label = label;
        }

        public int Value { get => value; set => this.value = value; }
        public string Label { get => label; set => label = value; }
    }
}
   
