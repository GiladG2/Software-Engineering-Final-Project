using Project_Gym.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Gym.View
{
    public partial class Test : System.Web.UI.Page
    {
        DBOperations.DBOperations dbOperations = new DBOperations.DBOperations();
        
        protected  void Page_Load(object sender, EventArgs e)
        {
            /*DataSet exercises = null;
            try {
            }
            catch
            {
                Controller_Exercises ce = new Controller_Exercises();
                 exercises = new DataSet();

                // Create a DataTable to hold the data
                DataTable exerciseTable = new DataTable("ExerciseData");

                // Add columns to the DataTable
                exerciseTable.Columns.Add("ExerciseID", typeof(int));
                exerciseTable.Columns.Add("ExerciseName", typeof(string));
                exerciseTable.Columns.Add("Repetitions", typeof(int));
                exerciseTable.Columns.Add("Duration", typeof(int)); // Duration in seconds

                // Add rows (data) to the DataTable
                exerciseTable.Rows.Add(1, "Push-Up", 20, 30);
                exerciseTable.Rows.Add(2, "Squat", 15, 40);
                exerciseTable.Rows.Add(3, "Plank", 0, 60); // No repetitions for Plank, only duration

                // Add the DataTable to the DataSet
                exercises.Tables.Add(exerciseTable);
            }
            finally
            {     
                exercises =   dbOperations.GetExerciseFromAPI();

            GridViewExercises.DataSource = exercises;
            GridViewExercises.DataBind();
            }*/
        }
    }
}