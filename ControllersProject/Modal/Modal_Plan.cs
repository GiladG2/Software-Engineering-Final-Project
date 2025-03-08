

// ControllersProject, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// ControllersProject.Modal.ControllersProject.Modal.Modal_Plan
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ControllersProject.Controller;
using Microsoft.Data.SqlClient;
using ControllersProject.DAL;
namespace ControllersProject.Modal
{
internal class Modal_Plan
{
    private Users_Controller cu = new Users_Controller();

    private Exercise_Controller ce = new Exercise_Controller();

    private User_Requests_Controller cur = new User_Requests_Controller();

    private const string file = "DB.mdf";

    public int GetDaysAweek(int user_id)
    {
        string query = "SELECT fldDays_A_Week FROM tblUser_Requests WHERE fldUser_Id = " + user_id;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return (dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["fldDays_A_Week"]) : 0;
    }

    public int GetDuration(int user_id)
    {
        string query = "SELECT fldDuration FROM tblUser_Requests WHERE fldUser_Id = " + user_id;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return (dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["fldDuration"]) : 0;
    }

    public int GetTypeOfTraining(int user_id)
    {
        string query = "SELECT fldTraining_Type FROM tblUser_Requests WHERE fldUser_Id = " + user_id;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return (dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["fldTraining_Type"]) : 0;
    }

    public int GetExeprience(int user_id)
    {
        string query = "SELECT fldExperience FROM tblUser_Requests WHERE fldUser_Id = " + user_id;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return (dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["fldExperience"]) : 0;
    }

    public Vector GetUserVector(int user_id, int userAge)
    {
        return cur.GetUserVector(user_id, userAge);
    }

    public string GetPlanCreationDate(int planId)
    {
        string query = "SELECT fldCreation_Date FROM tblPlans WHERE fldPlan_Id = " + planId;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return (dataTable.Rows.Count > 0) ? dataTable.Rows[0]["fldCreation_Date"].ToString() : string.Empty;
    }

        public int GetAmoutOflogged(int planId, int period = 2)
        {
            string text = @"
WITH RankedExercises AS (
    SELECT fldDate, fldExercise_Id,
           ROW_NUMBER() OVER (PARTITION BY fldExercise_Id, fldDate ORDER BY fldOrder ASC) AS rn
    FROM tblExercises_Worked_In_Workout
    WHERE fldPlan_Id = " + planId + @"
)
SELECT COUNT(*) AS DaysWithTwoExercises
FROM (
    SELECT fldDate
    FROM RankedExercises
    WHERE rn = 1
    GROUP BY fldDate
    HAVING COUNT(DISTINCT fldExercise_Id) >= 2
) AS SubQuery";

            // Get the date based on the period
            DateTime dateTime = DateTime.Now;
            switch (period)
            {
                case 1:
                    dateTime = dateTime.AddMonths(-1);
                    break;
                case 2:
                    dateTime = dateTime.AddMonths(-3);
                    break;
                case 3:
                    dateTime = dateTime.AddMonths(-6);
                    break;
                case 4:
                    dateTime = dateTime.AddYears(-1);
                    break;
            }

            // Ensure the WHERE condition is appended properly to the main query
            string query = text + " WHERE fldDate >= '" + dateTime.ToString("yyyy-MM-dd") + "'";

            DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);

            return (dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["DaysWithTwoExercises"]) : 0;
        }

        public Exercise GetExercise(string muscleName, double difficulty)
    {
        int num = 0;
        string query = "SELECT * FROM tblMuscles_List WHERE fldMuscle_Name = '" + muscleName + "'";
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        if (dataTable.Rows.Count != 0)
        {
            num = Convert.ToInt32(dataTable.Rows[0]["fldMuscle_Id"]);
        }
        query = ((difficulty != -1.0) ? ("SELECT * FROM tblExercise_List WHERE fldExercise_Id IN (SELECT fldExercise_Id FROM tblMuscles_Worked_In_Exercises WHERE fldMuscle_Id = " + num + ") AND fldDifficulty = " + difficulty + " ORDER BY NEWID()") : ("SELECT * FROM tblExercise_List WHERE fldExercise_Id IN (SELECT fldExercise_Id FROM tblMuscles_Worked_In_Exercises WHERE fldMuscle_Id = " + num + ") ORDER BY NEWID()"));
        dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        if (dataTable.Rows.Count == 0)
        {
            return null;
        }
        int id = Convert.ToInt32(dataTable.Rows[0]["fldExercise_Id"]);
        int time_To_Complete = Convert.ToInt32(dataTable.Rows[0]["fldTime_To_Complete"]);
        string name = dataTable.Rows[0]["fldExercise_Name"].ToString();
        string instructions = dataTable.Rows[0]["fldExercise_Desc"].ToString();
        if (difficulty == -1.0)
        {
            difficulty = Convert.ToDouble(dataTable.Rows[0]["fldDifficulty"]);
        }
        query = "SELECT fldMuscle_Id FROM tblMuscles_Worked_In_Exercises WHERE fldExercise_Id = " + id;
        dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        LinkedList<int> linkedList = new LinkedList<int>();
        if (dataTable.Rows.Count > 0)
        {
            linkedList.AddFirst(new LinkedListNode<int>(Convert.ToInt32(dataTable.Rows[0]["fldMuscle_Id"])));
        }
        return new Exercise(id, name, instructions, difficulty, time_To_Complete, linkedList);
    }

    public bool UserFiledARequest(int user_id)
    {
        string query = "SELECT * FROM tblUser_Requests WHERE fldUser_Id = " + user_id;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return dataTable.Rows.Count != 0;
    }

    public int GetWorkoutIdByName(string workoutName)
    {
        return workoutName switch
        {
            "Legs - Quadriceps, Hamstrings and Calves" => 1,
            "Push - Chest, Shoulders and Triceps" => 2,
            "Pull - Lats and Middle back" => 3,
            _ => 0,
        };
    }

    public bool UserHasLogged(int planId, int exerciseId)
    {
        string query = "SELECT * FROM tblExercises_Worked_In_Workout WHERE fldPlan_Id = " + planId + " AND fldExercise_Id = " + exerciseId;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return dataTable.Rows.Count > 0;
    }

    public void InsertPlan(LinkedList<Workout> plan, int userId)
    {
        string query = "INSERT INTO tblPlans(fldUser_Id, fldCreation_Date) VALUES(" + userId + ", CAST(GETDATE() AS DATE))";
        AdoHelper.CheckInsert("DB.mdf", query);
        query = "SELECT MAX(fldPlan_Id) AS fldPlan_Id FROM tblPlans WHERE fldUser_Id = " + userId;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        int num = Convert.ToInt32(dataTable.Rows[0][0]);
        foreach (Workout item in plan)
        {
            int workoutIdByName = GetWorkoutIdByName(item.Name);
            LinkedList<Exercise> exerciseList = item.ExerciseList;
            int num2 = 1;
            foreach (Exercise item2 in exerciseList)
            {
                query = "INSERT INTO tblExercises_In_Plan (fldPlan_Id, fldExercise_Id, fldDayOfExercise, fldWorkout_Id, fldExercise_Order) VALUES(" + num + ", " + item2.Id + ", 0, " + workoutIdByName + ", " + num2++ + ")";
                try
                {
                    AdoHelper.CheckInsert("DB.mdf", query);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting exercise: " + ex.Message);
                }
            }
        }
    }

    public bool HasAPlan(int userId)
    {
        string query = "SELECT * FROM tblPlans WHERE fldUser_Id = " + userId;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return dataTable.Rows.Count != 0;
    }

    public string GetWorkoutNameById(int id)
    {
        string query = "SELECT fldWorkout_Name FROM tblWorkoutType WHERE fldWorkout_Id = " + id;
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        return (dataTable.Rows.Count != 0) ? dataTable.Rows[0]["fldWorkout_Name"].ToString() : string.Empty;
    }

    public string GetWorkoutDetails(int planId)
    {
        string query = $"SELECT fldWorkoutDetails FROM tblPlans WHERE fldPlan_Id = {planId}";
        return AdoHelper.GetDataTable("DB.mdf", query).Rows[0]["fldWorkoutDetails"].ToString();
    }

    public bool InsertPlan(int userId, string workoutDetails)
    {
        string query = $"INSERT INTO tblPlans (user_id, fldWorkoutDetails, fldCreation_Date) VALUES ({userId}, '{workoutDetails}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
        int num = AdoHelper.CheckInsert("DB.mdf", query);
        return num > 0;
    }

    public bool IsUserRequestExists(int userId)
    {
        string query = $"SELECT COUNT(*) FROM tblRequests WHERE user_id = {userId}";
        int num = Convert.ToInt32(AdoHelper.GetDataTable("DB.mdf", query).Rows[0][0]);
        return num > 0;
    }

    public bool InsertRequest(int userId, string requestDetails)
    {
        string query = $"INSERT INTO tblRequests (user_id, fldRequestDetails, fldRequestDate) VALUES ({userId}, '{requestDetails}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";
        int num = AdoHelper.CheckInsert("DB.mdf", query);
        return num > 0;
    }

    public bool UpdateRequest(int requestId, string newRequestDetails)
    {
        string query = $"UPDATE tblRequests SET fldRequestDetails = '{newRequestDetails}' WHERE fldRequestId = {requestId}";
        int num = AdoHelper.CheckInsert("DB.mdf", query);
        return num > 0;
    }

    public bool DeleteRequest(int requestId)
    {
        string query = $"DELETE FROM tblRequests WHERE fldRequestId = {requestId}";
        int num = AdoHelper.CheckInsert("DB.mdf", query);
        return num > 0;
    }

    public bool InsertExerciseToPlan(int planId, int exerciseId)
    {
        string query = $"INSERT INTO tblExercises_In_Plan (fldPlanId, fldExerciseId) VALUES ({planId}, {exerciseId})";
        int num = AdoHelper.CheckInsert("DB.mdf", query);
        return num > 0;
    }

    public bool DeleteExerciseFromPlan(int planId, int exerciseId)
    {
        string query = $"DELETE FROM tblExercises_In_Plan WHERE fldPlanId = {planId} AND fldExerciseId = {exerciseId}";
        int num = AdoHelper.CheckInsert("DB.mdf", query);
        return num > 0;
    }

    public bool DeletePlan(int userId)
    {
        string query = $"DELETE FROM tblPlans WHERE fldUser_Id = {userId}";
        return AdoHelper.CheckInsert("DB.mdf", query) > 0;
    }

    public void UpdatePlan(int planId, int exerciseId, int newExerciseId)
    {
        //IL_001d: Unknown result type (might be due to invalid IL or missing references)
        //IL_0023: Expected O, but got Unknown
        string text = "\r\n            UPDATE tblExercises_In_Plan\r\n            SET fldExercise_Id = @NewExerciseId\r\n            WHERE fldPlan_Id = @PlanId AND fldExercise_Id = @ExerciseId";
        SqlConnection val = AdoHelper.ConnectToDb("DB.mdf");
        try
        {
            try
            {
                ((DbConnection)(object)val).Open();
                SqlCommand val2 = new SqlCommand(text, val);
                try
                {
                    val2.Parameters.AddWithValue("@PlanId", (object)planId);
                    val2.Parameters.AddWithValue("@ExerciseId", (object)exerciseId);
                    val2.Parameters.AddWithValue("@NewExerciseId", (object)newExerciseId);
                    int num = ((DbCommand)(object)val2).ExecuteNonQuery();
                    if (num > 0)
                    {
                        Console.WriteLine($"Exercise {exerciseId} successfully updated to {newExerciseId} in plan {planId}.");
                    }
                    else
                    {
                        Console.WriteLine("No rows updated. Please check if the exercise and plan exist.");
                    }
                }
                finally
                {
                    ((IDisposable)val2)?.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating the plan: " + ex.Message);
                throw;
            }
            finally
            {
                if (((DbConnection)(object)val).State == ConnectionState.Open)
                {
                    ((DbConnection)(object)val).Close();
                }
            }
        }
        finally
        {
            ((IDisposable)val)?.Dispose();
        }
    }

    public int GetPlanIdByUser(int userId)
    {
        string query = $"SELECT MAX(fldPlan_Id) AS fldPlan_Id FROM tblPlans WHERE fldUser_Id = {userId};";
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        int result = 0;
        if (dataTable.Rows.Count > 0)
        {
            result = Convert.ToInt32(dataTable.Rows[0][0]);
        }
        return result;
    }

    public int GetUserIdByPlan(int planId)
    {
        string query = $"SELECT fldUser_Id AS fldPlan_Id FROM tblPlans WHERE fldPlan_Id = {planId}";
        DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
        int result = 0;
        if (dataTable.Rows.Count > 0)
        {
            result = Convert.ToInt32(dataTable.Rows[0][0]);
        }
        return result;
    }

    public LinkedList<Workout> GetPlan(int userId)
    {
        int planIdByUser = GetPlanIdByUser(userId);
        LinkedList<Workout> linkedList = new LinkedList<Workout>();
        int num = 1;
        while (true)
        {
            string workoutNameById = GetWorkoutNameById(num);
            string query = $"\r\n            SELECT *\r\nFROM tblExercise_List\r\nJOIN tblExercises_In_Plan ON tblExercise_List.fldExercise_Id = tblExercises_In_Plan.fldExercise_Id\r\nWHERE tblExercises_In_Plan.fldPlan_Id = {planIdByUser}\r\n  AND tblExercises_In_Plan.fldWorkout_Id = {num}\r\nORDER BY tblExercises_In_Plan.fldExercise_Order;\r\n";
            DataTable dataTable = AdoHelper.GetDataTable("DB.mdf", query);
            if (dataTable.Rows.Count == 0)
            {
                break;
            }
            Workout workout = new Workout(workoutNameById);
            LinkedList<Exercise> exerciseList = workout.ExerciseList;
            foreach (DataRow row in dataTable.Rows)
            {
                int num2 = Convert.ToInt32(row["fldExercise_Id"]);
                string name = row["fldExercise_Name"].ToString();
                string instructions = row["fldExercise_Desc"].ToString();
                int num3 = Convert.ToInt32(row["fldDifficulty"]);
                int time_To_Complete = ((!Convert.IsDBNull(row["fldTime_To_Complete"])) ? Convert.ToInt32(row["fldTime_To_Complete"]) : 0);
                LinkedList<int> musclesWorked = ce.GetMusclesWorked(num2);
                Exercise value = new Exercise(num2, name, instructions, num3, time_To_Complete, musclesWorked);
                exerciseList.AddLast(value);
            }
            linkedList.AddLast(workout);
            num++;
        }
        return linkedList;
    }
}

}

