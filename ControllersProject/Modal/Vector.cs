using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class Vector
    {
        private double x;//x - Experience
        private double y;//y - Duration
        private double z;//z - Type of training
        private double length;
        private double score;

        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.length = CalculateMagnitude();
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
        public double Length { get => length; set => length = value; }
        public double Score { get => score; set => score = value; }

        private double CalculateMagnitude()
        {
            return Math.Sqrt(x * x + z * z + y * y);
        }
        private static double AngleBetweenTwoVectors(Vector v1, Vector v2)
        {
            double angle = 0.0;
            double dotProduct = v1.X * v2.X + v2.Y * v1.Y + v1.Z * v2.Z;
            angle = Math.Acos(dotProduct / (v1.Length * v2.length));
            return angle * 180 / Math.PI;
        }
        //טענת כניסה : הפעולה מקבלת שני ווקטורים
        //טענת יציאה: הפעולה תחזיר ציון, ככל שהוא יותר גבוהה, הווקטורים יותר קרובים אחד לשני
        //ככל שהציון נמוך יותר, הווקטורים רחוקים יותר אחד מהשני
        public static double GetDotProduct(Vector v1, Vector v2)
        { 
            if (v1.Length == 0 || v2.length == 0)
                return 0;
            const int a = 3,
                      b = 2;
            double score = a * Math.Cos(AngleBetweenTwoVectors(v1, v2)) - b * Math.Abs((v1.Length - v2.Length));
            return score;
        }
    }
}

