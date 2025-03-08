using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllersProject.DAL;
namespace ControllersProject.Modal
{
    internal class Modal_Progression
    {
        private readonly string file = "DB.mdf";
        public double GetProgression(int planId, int period, int exerciseId)
        {

            string baseQuery = $@"
    WITH RankedExercises AS (
        SELECT * ,
               ROW_NUMBER() OVER (PARTITION BY fldExercise_Id, fldDate ORDER BY fldOrder ASC) AS rn
        FROM tblExercises_Worked_In_Workout
        WHERE fldExercise_Id = {exerciseId} AND fldPlan_Id = {planId}
";
            DateTime currentDate = DateTime.Now;
            DateTime startDate = DateTime.MinValue;

            // Add the date filter condition based on the period
            if (period == 1) // Last month
            {
                startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            }
            else if (period == 2) // Last 3 months
            {
                startDate = currentDate.AddMonths(-3);
            }
            else if (period == 3) // Last 6 months
            {
                startDate = currentDate.AddMonths(-6);
            }
            else if (period == 4) // Last year
            {
                startDate = currentDate.AddYears(-1);
            }

            // Add the date filter if a valid startDate is determined
            if (startDate != DateTime.MinValue)
            {
                baseQuery += $" AND fldDate >= '{startDate:yyyy-MM-dd}'";
            }

            // Close the CTE
            baseQuery += @"
    )
    SELECT fldDate, fldReps, fldWeight_Kg
    FROM RankedExercises
    WHERE rn = 1
    ORDER BY fldDate ASC; -- Change DESC to ASC for chronological order
";
            LinkedList<int> volumeList = new LinkedList<int>();
            LinkedList<int> dates = new LinkedList<int>();
            // Execute the query
            DataTable dt = AdoHelper.GetDataTable(file,baseQuery);
            foreach (DataRow dataRow in dt.Rows)
            {
                int fldReps = Convert.ToInt32(dataRow["fldReps"]);
                int fldWeightKg = Convert.ToInt32(dataRow["fldWeight_Kg"]);

                int volume = fldReps * fldWeightKg;
                volumeList.AddLast(volume);
                dates.AddLast(GetMonthDifference(Convert.ToDateTime(dataRow["fldDate"])));
            }
            if (volumeList.Count < 2)
                return 0;
            int firstVL = volumeList.First();
            int lastVL = volumeList.Last();
            int firstDate = dates.First();
            int lastDate = dates.Last();
            if (firstDate - lastDate == 0)
                return lastVL - firstVL;
            return ((double)lastVL - firstVL) / Math.Abs(lastDate - firstDate);
        }
        private int GetMonthDifference(DateTime toDate)
        {
            DateTime startDate = DateTime.Now;
            int yearsDifference = Math.Abs(toDate.Year - startDate.Year);
            int monthsDifference = Math.Abs(toDate.Month - startDate.Month);
            if (toDate.Day < startDate.Day)
                monthsDifference--;
            monthsDifference += yearsDifference * 12;
            return monthsDifference;
        }
    }
}
