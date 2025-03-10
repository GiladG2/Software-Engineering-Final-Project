using ControllersProject.Controller;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ControllersProject.DAL;
using NuGet.Common;


namespace ControllersProject.Modal
{
    internal class Modal_Training_Log
    {
        private Modal_Users mu = new Modal_Users();
        private Exercise_Controller me = new Exercise_Controller();
        private const string file = "DB.mdf";

        public string GetLog(string date, int plan_Id)
        {
            string workoutlog = "";
            string totalWorkoutLog = "";
            string query = $"SELECT * FROM tblExercises_Worked_In_Workout WHERE fldDate = '{date}' AND fldPlan_Id = {plan_Id}";
            DataTable dt = mu.SelectData(query);
            if (dt.Rows.Count == 0)
            {
                totalWorkoutLog = "<div>";
                totalWorkoutLog += "<h2>Add an exercise!</h2>";
                totalWorkoutLog += "</div>";
            }
            string exerciseName;
            int exerciseId;
            int order;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                exerciseId = int.Parse(dt.Rows[i]["fldExercise_Id"].ToString());
                order = int.Parse(dt.Rows[i]["fldOrder"].ToString());
                exerciseName = me.GetExerciseName(exerciseId);
                workoutlog = $"<div class=\"exercise-box\" id='exercise-box-{order}'>";
                workoutlog += $"<h3 class='exercise-name'>{exerciseName}</h3>";
                workoutlog += $"<p> Reps: <span class='exercise-reps'>{dt.Rows[i]["fldReps"].ToString()}</span></p>";
                workoutlog += $"<p>Weight: <span class=\"exercise-weight\">{dt.Rows[i]["fldWeight_Kg"]}</span> kg</p>";
                workoutlog += $"<button onclick = 'editExercise({exerciseId},{plan_Id},{date},{order})' class=\"edit-btn\">Edit</button>";
                workoutlog += $"<button onclick = 'deleteExercise({exerciseId},{plan_Id},{date},{order})' class=\"delete-btn\">Delete</button>";
                workoutlog += "<hr />";
                workoutlog += "<br />";
                workoutlog += "</div>";
                totalWorkoutLog += workoutlog;
            }
            return totalWorkoutLog;
        }

        public List<TrainingLogExerciseFormat> GetLogForAPI(int planId,string date)
        {
            string query = $"SELECT * FROM tblExercises_Worked_In_Workout WHERE fldDate = '{date}' AND fldPlan_Id = {planId}";
            DataTable dt = mu.SelectData(query);
            List<TrainingLogExerciseFormat> list = new List<TrainingLogExerciseFormat>();
            string exerciseName;
            int exerciseId;
            int order;
            int reps;
            double weight;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                exerciseId = int.Parse(dt.Rows[i]["fldExercise_Id"].ToString());
                exerciseName = me.GetExerciseName(exerciseId);
                order = int.Parse(dt.Rows[i]["fldOrder"].ToString());
                reps = int.Parse(dt.Rows[i]["fldReps"].ToString());
                weight = double.Parse(dt.Rows[i]["fldWeight_Kg"].ToString());
                list.Add(new TrainingLogExerciseFormat(exerciseName,exerciseId,order,reps,weight));
            }
            return list;
        }
        public static bool SaveExercise(int planId, int reps, int weight, int exerciseId, int userId, string date)
        {
            string file = "DB.mdf";
            string query;
            int check = 0;
            if (!CheckLogged(planId, date))
            {
                query = $"INSERT INTO tblWorkout_Log (fldPlan_Id, fldDate, fldUser_Id) " +
                        $"VALUES ({planId}, '{date}', {userId})";
                check = AdoHelper.CheckInsert(file,query);
                if (check <= 0)
                    return false;
            }

            int currentOrder = check != 0 ? 1 : GetMaxOrder(planId, date) + 1;
            query = $"INSERT INTO tblExercises_Worked_In_Workout (fldPlan_Id,fldDate, fldExercise_Id, fldReps,fldOrder,fldWeight_Kg,fldWeight_Lbs) " +
                      $"VALUES ({planId},'{date}', '{exerciseId}', {reps}, {currentOrder}, {weight}, {weight * 2.2})";
            check = AdoHelper.CheckInsert(file, query);

            return check > 0;
        }
        public static int GetMaxOrder(int planId, string date)
        {
            string file = "DB.mdf";
            string query = $"SELECT MAX(fldOrder) AS MaxOrder FROM tblExercises_Worked_In_Workout WHERE fldPlan_Id = {planId} AND fldDate = '{date}'";

            DataTable dt = AdoHelper.GetDataTable(file,query);

            if (dt != null && dt.Rows.Count > 0)
            {
                var maxOrder = dt.Rows[0]["MaxOrder"];
                if (maxOrder != DBNull.Value)
                {
                    return Convert.ToInt32(maxOrder);
                }
            }
            return 0;
        }


        public static bool CheckLogged(int planId, string currentDateString)
        {
            string file = "DB.mdf";
            string query = $"SELECT * FROM tblWorkout_Log WHERE fldDate = '{currentDateString}' AND fldPlan_Id = {planId}";
            DataTable dt = AdoHelper.GetDataTable(file,query);
            if (dt.Rows.Count == 0)
                return false;
            return true;
        }

        public static string GetGraphData(int exerciseId, int planId, int period = 0)
        {

            string file = "DB.mdf";
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
                startDate = currentDate.AddMonths(-1);
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

            // Execute the query
            DataTable dt = AdoHelper.GetDataTable(file,baseQuery);



            // Prepare the list to return in JSON format
            var result = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime fldDate = Convert.ToDateTime(row["fldDate"]);
                int fldReps = Convert.ToInt32(row["fldReps"]);
                decimal fldWeightKg = Convert.ToDecimal(row["fldWeight_Kg"]);

                decimal volume = fldReps * fldWeightKg;

                // Format and add to the result list
                result.Add(new
                {
                    date = fldDate.ToString("yyyy-MM-dd"),  // Format date as 'yyyy-MM-dd'
                    volume = volume
                });
            }

            // Return the result as JSON
            return JsonConvert.SerializeObject(result);
        }

        public List<GraphDataFormat> GetGraphDataAPI(int exerciseId, int planId, int period = 0)
        {
            string file = "DB.mdf";
            string query = $@"
        WITH RankedExercises AS (
            SELECT *,
                   ROW_NUMBER() OVER (PARTITION BY fldExercise_Id, fldDate ORDER BY fldOrder ASC) AS rn
            FROM tblExercises_Worked_In_Workout
            WHERE fldExercise_Id = {exerciseId} AND fldPlan_Id = {planId}";

            DateTime currentDate = DateTime.Now;
            DateTime startDate = DateTime.MinValue;

            // Apply the date filter based on the period parameter.
            if (period == 1) // Last month
            {
                startDate = currentDate.AddMonths(-1);
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

            // Append the date filter if a valid startDate was set.
            if (startDate != DateTime.MinValue)
            {
                query += $" AND fldDate >= '{startDate:yyyy-MM-dd}'";
            }

            // Close the CTE and build the main query.
            query += @"
        )
        SELECT fldDate, fldReps, fldWeight_Kg 
        FROM RankedExercises 
        WHERE rn = 1
        ORDER BY fldDate ASC;
    ";

            DataTable dt = AdoHelper.GetDataTable(file, query);
            List<GraphDataFormat> result = new List<GraphDataFormat>();

            foreach (DataRow row in dt.Rows)
            {
                DateTime fldDate = Convert.ToDateTime(row["fldDate"]);
                int fldReps = Convert.ToInt32(row["fldReps"]);
                decimal fldWeightKg = Convert.ToDecimal(row["fldWeight_Kg"]);

                decimal volume = fldReps * fldWeightKg;

                // Create the object with both volume and the formatted date.
                result.Add(new GraphDataFormat((int)volume, fldDate.ToString("yyyy-MM-dd")));
            }

            return result;
        }

        public void SaveLogChanges(int exerciseId, int planId, string date, int order, int reps, double weightKg)
        {
            // Convert weight to lbs (1 kg = 2.20462 lbs)
            double weightLbs = weightKg * 2.20462;

            // Build the UPDATE SQL query
            string query = @"
        UPDATE tblExercises_Worked_In_Workout
        SET fldReps = @reps, fldWeight_Kg = @weightKg, fldWeight_Lbs = @weightLbs
        WHERE fldPlan_Id = @planId AND fldExercise_Id = @exerciseId AND fldDate = @date AND fldOrder = @order;
    ";

            // Execute the query
            using (var connection = AdoHelper.ConnectToDb(file))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query
                    command.Parameters.AddWithValue("@planId", planId);
                    command.Parameters.AddWithValue("@exerciseId", exerciseId);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@order", order);
                    command.Parameters.AddWithValue("@reps", reps);
                    command.Parameters.AddWithValue("@weightKg", weightKg);
                    command.Parameters.AddWithValue("@weightLbs", weightLbs);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool SaveLogChangesAPI(int exerciseId, int planId, string date, int order, int reps, double weightKg)
        {
            // Convert weight to lbs (1 kg = 2.20462 lbs)
            double weightLbs = weightKg * 2.20462;

            // Build the UPDATE SQL query
            string query = @"
    UPDATE tblExercises_Worked_In_Workout
    SET fldReps = @reps, fldWeight_Kg = @weightKg, fldWeight_Lbs = @weightLbs
    WHERE fldPlan_Id = @planId AND fldExercise_Id = @exerciseId AND fldDate = @date AND fldOrder = @order;
";

            // Execute the query
            using (var connection = AdoHelper.ConnectToDb(file))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query
                    command.Parameters.AddWithValue("@planId", planId);
                    command.Parameters.AddWithValue("@exerciseId", exerciseId);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@order", order);
                    command.Parameters.AddWithValue("@reps", reps);
                    command.Parameters.AddWithValue("@weightKg", weightKg);
                    command.Parameters.AddWithValue("@weightLbs", weightLbs);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if at least one row was updated
                }
            }
        }

        public string DeleteLoggedExercise(int exerciseId, int planId, string date, int order)
        {
            // Using parameterized queries to prevent SQL injection and improve security
            string str = "DELETE FROM tblExercises_Worked_In_Workout WHERE fldPlan_Id = @planId AND fldDate = @date AND fldOrder = @order";

            using (SqlConnection connection = AdoHelper.ConnectToDb(file))
            {
                using (SqlCommand command = new SqlCommand(str, connection))
                {
                    // Use parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@planId", planId);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@order", order);

                    connection.Open();

                    // Execute the DELETE statement
                    int rowsAffected = command.ExecuteNonQuery();  // This returns the number of rows affected

                    // Return a message based on whether rows were deleted
                    return rowsAffected > 0 ? "deleted" : "not";
                }
            }
        }

    }
}
