using ControllersProject.Controller;
using PlanDataWebService.Controller;
using PlanDataWebService.DBServer;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient; // For database connection
using System.Xml.Linq;

namespace PlanDataWebService.View
{
    public partial class AddExercises : System.Web.UI.Page
    {
        static string serverDB = "ServerDB.mdf"; // Adjust path as needed
        private Exercise_Controller ec = new Exercise_Controller(serverDB);
        private Muscles_Controller mc = new Muscles_Controller(serverDB);
        public string msg = "";
        public string dropDown = "";

        //עצם מטיפוס מחלקת שירות הרשת
        private DBOperations dBOperations = new DBOperations();
        protected async void Page_Load(object sender, EventArgs e)
        {
            
            Analytics_Controller ac = new Analytics_Controller();//עצם האחראי על עדכון סטטיסטיקת המשתמש שערך את מסד הנתונים
            dropDown = mc.GetMusclesForDropdown();//קבלת כל השרירים הקיימים במסד הנתונים להצגה
            if (Request.Form["submit"] != null)
            {
                string username = Session["username"].ToString();//session קבלת שם המשתמש מ 
                Controller_Users cu = new Controller_Users();
                int userId = cu.GetUserIdByUsername(username);//קבלת מספר הזהות של המשתמש לפי שם המשתמש שלו
                //קבלת הנתונים מהטופס שמילא המשתמש להוספת תרגיל
                string exerciseName = Request.Form["exercise_name"];
                string exerciseDesc = Request.Form["exercise_desc"];
                double difficulty = double.Parse(Request.Form["difficulty"]);
                int timeToComplete = int.Parse(Request.Form["time_to_complete"]);
                int muscleId = int.Parse(Request.Form["muscle"]);
                // הוספת התרגיל למסד הנתונים המשני
                bool success = ec.AddExercise(exerciseName, exerciseDesc, difficulty, timeToComplete, muscleId);
                // Optionally provide user feedback
                if (success)
                {
                    msg = "Exercise added successfully!";     
                    //הוספת התרגיל למסד הנתונים הראשי על ידי שימוש במחלקת שירות הרשת
                    dBOperations.AddNewExercise(exerciseName, exerciseDesc, difficulty, timeToComplete, muscleId);
                    Response.Write("success");
                    Response.Write("Error");
                    ac.AddInsert(userId);
                }
                    

                
                /*catch (Exception ex)
                {
                    // Log error and notify the user
                    Console.WriteLine("Error: " + ex.Message);
                    msg = "Error: " + ex.Message;
                         
                        }*/
            }
        }
    }
}
