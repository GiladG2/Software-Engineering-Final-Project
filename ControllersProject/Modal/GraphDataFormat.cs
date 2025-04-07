using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class GraphDataFormat
    {
        int value;//Saves the y axis value
        string date;//Saves the x axis value
        public GraphDataFormat(int value, string date)
        {
            this.value = value;
            this.date = date;
        }   

        public int Value { get => value; set => this.value = value; }
        public string Date { get => date; set => date = value; }
    }
}
