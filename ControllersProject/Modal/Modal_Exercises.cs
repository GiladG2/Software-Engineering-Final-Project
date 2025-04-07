using System;
using System.Data;
using System.Collections.Generic;
using ControllersProject.Controller;
using ControllersProject.DAL;
using System.Numerics;

namespace ControllersProject.Modal
{
    internal class Modal_Exercises
    {
        // A helper object to execute ADO.NET queries
        private readonly AdoHelper _adoHelper = new AdoHelper();
        // Database file name used for some queries
        private readonly string file = "DB.mdf";
        // Controller for managing users
        private Users_Controller cu = new Users_Controller();
        // Controller for managing user requests
        private User_Requests_Controller cur = new User_Requests_Controller();
        // Controller for managing muscles
        private Muscles_Controller mc = new Muscles_Controller();

        // Constructor that accepts the full database path and initializes the AdoHelper accordingly.
        public Modal_Exercises(string dbName)
        {
            _adoHelper = new AdoHelper(dbName);
        }

        // Default constructor.
        public Modal_Exercises()
        {

        }

        // Returns the next available exercise ID by getting the maximum current ID from the exercise list.
        public int GetNextExerciseId()
        {
            string query = "SELECT ISNULL(MAX(fldExercise_Id), 0) + 1 AS NextId FROM tblExercise_List";
            DataTable dt = _adoHelper.GetDataTable(query);
            return dt.Rows.Count > 0 ? int.Parse(dt.Rows[0]["NextId"].ToString()) : 1;
        }

        // Retrieves all exercises from the tblExercise_List table as a DataSet.
        public DataSet GetAllExercises()
        {
            string query = "SELECT * FROM tblExercise_List";
            return _adoHelper.GetDataSet(query);
        }

        // Deletes an exercise from the tblExercise_List table based on the given exercise ID.
        public bool DeleteExercise(int exerciseId)
        {
            string query = $"DELETE FROM tblExercise_List WHERE fldExercise_Id = {exerciseId}";
            return _adoHelper.CheckInsert(query) > 0;
        }

        // Inserts a new exercise into the tblExercise_List table with the provided details.
        public bool AddExercise(string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            string query = $@"
                INSERT INTO tblExercise_List (fldExercise_Name, fldExercise_Desc, fldDifficulty, fldTime_To_Complete)
                VALUES ('{exerciseName}', '{exerciseDesc}', {difficulty}, {timeToComplete});
            ";
            return _adoHelper.CheckInsert(query) > 0;
        }

        // Associates a muscle with an exercise by inserting a record into tblMuscles_Worked_In_Exercises.
        public bool AddMuscleToExercise(int exerciseId, int muscleId)
        {
            string query = $@"
                INSERT INTO tblMuscles_Worked_In_Exercises (fldExercise_Id, fldMuscle_Id)
                VALUES ({exerciseId}, {muscleId});
            ";
            return _adoHelper.CheckInsert(query) > 0;
        }

        // Updates an existing exercise's details in the tblExercise_List table.
        public bool EditExercise(int exerciseId, string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            string query = $@"
                UPDATE tblExercise_List
                SET fldExercise_Name = '{exerciseName}',
                    fldExercise_Desc = '{exerciseDesc}',
                    fldDifficulty = {difficulty},
                    fldTime_To_Complete = {timeToComplete}
                WHERE fldExercise_Id = {exerciseId};
            ";
            return _adoHelper.CheckInsert(query) > 0;
        }

        // **New Missing Functions**:

        // Retrieves a list of muscle IDs that are worked by a specific exercise.
        public LinkedList<int> GetMusclesWorked(int exerciseId)
        {
            LinkedList<int> musclesWorked = new LinkedList<int>();
            string query = $"SELECT fldMuscle_Id FROM tblMuscles_Worked_In_Exercises WHERE fldExercise_Id = {exerciseId}";
            DataTable dt = _adoHelper.GetDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                int muscleId = int.Parse(dr["fldMuscle_Id"].ToString());
                musclesWorked.AddLast(muscleId);
            }
            return musclesWorked;
        }

        // Retrieves all exercises that work a given muscle name and sorts them based on a user vector's dot product.
        public LinkedList<Exercise> AllExercises(string muscleName, Vector userVector)
        {
            //שליפת מספר הזהות של שריר לפי השם שלו
            int muscleId = 0;
            string query = $"SELECT * FROM tblMuscles_List WHERE fldMuscle_Name = '{muscleName}'";
            DataTable dt = _adoHelper.GetDataTable(query);
            if (dt.Rows.Count != 0)
                muscleId = int.Parse((dt.Rows[0]["fldMuscle_Id"]).ToString());
            //שליפת כל התרגילים העובדים על אותו שריר
            query = $"SELECT * FROM tblExercise_List WHERE fldExercise_Id IN " +
             $"(SELECT fldExercise_Id FROM tblMuscles_Worked_In_Exercises WHERE fldMuscle_Id = {muscleId}) ";
            DataTable exerciseDt = _adoHelper.GetDataTable(query);
            // רשימה התשמור בסופו של דבר רשימה ממוינת של כל התרגילים לפי התאמתם למשתמש
            LinkedList<Exercise> allExercises = new LinkedList<Exercise>();
            foreach (DataRow row in exerciseDt.Rows)
            {
                //קבלת נתוני תרגיל ספציפי
                int id = int.Parse(row["fldExercise_Id"].ToString());
                string name = row["fldExercise_Name"].ToString();
                string instructions = row["fldExercise_Desc"].ToString();
                double difficulty = double.Parse(row["fldDifficulty"].ToString());
                int timeToComplete = int.Parse(row["fldTime_To_Complete"].ToString());
                //שומר את סוג קבוצת השרירים של השרירים העובדים על התרגיל
                int type = int.Parse(row["fldMuscleGroup"].ToString());
                LinkedList<int> musclesWorked = new LinkedList<int>();
                string inheritanceQuery = "";
                Exercise exercise = null;//עצם מטיפוס תרגיל השומר את התרגיל הנוכחי
                DataTable secondaryDT;
                //שימוש בפולימורפיזם על מנת להשתמש בפעולות לחישוב הווקטור
                //הייחודיות לכל אחד מסוגי התרגילים השונים
                if (type == 0)
                {
                    inheritanceQuery = $"SELECT * FROM tblLeg_Exercises WHERE fldExercise_Id " +
                     $"IN(SELECT fldExercise_Id from tblExercise_List WHERE fldMuscleGroup = {type} " +
                     $"AND fldExercise_Id= {id})";
                    secondaryDT = _adoHelper.GetDataTable(inheritanceQuery);
                    double kneeDamage = double.Parse(secondaryDT.Rows[0]["fldKnee_Damage"].ToString());
                    bool isSquat = Convert.ToBoolean(secondaryDT.Rows[0]["fldIs_Squat_Based"]);
                    exercise = new LegExercise(id, name, instructions, difficulty, timeToComplete,
                        musclesWorked, kneeDamage, isSquat);
                }
                if (type == 1)
                {
                    inheritanceQuery = $"SELECT * FROM tblPush_Exercises WHERE fldExercise_Id" +
                        $" IN(SELECT fldExercise_Id from tblExercise_List WHERE fldMuscleGroup = {type} " +
                        $"AND fldExercise_Id= {id})";
                    secondaryDT = _adoHelper.GetDataTable(inheritanceQuery);
                    double elbowDamage = double.Parse(secondaryDT.Rows[0]["fldElbow_Damage"].ToString());
                    double explosiveness = double.Parse(secondaryDT.Rows[0]["fldExplosiveness"].ToString());
                    exercise = new PushExercise(id, name, instructions, difficulty, timeToComplete,
                        musclesWorked, elbowDamage, explosiveness);
                }
                if (type == 2)
                {
                    inheritanceQuery = $"SELECT * FROM tblArmsExercise WHERE fldExercise_Id" +
                        $" IN(SELECT fldExercise_Id from tblExercise_List WHERE fldMuscleGroup = {type} " +
                        $"AND fldExercise_Id= {id})";
                    secondaryDT = _adoHelper.GetDataTable(inheritanceQuery);
                    bool isloatesMuscle = Convert.ToBoolean(secondaryDT.Rows[0]["fldIsolatesMuscle"]);
                    double wristStress = double.Parse(secondaryDT.Rows[0]["fldWristStress"].ToString());
                    int gripWidth = int.Parse(secondaryDT.Rows[0]["fldGripWidth"].ToString());
                    exercise = new ArmExercise(id, name, instructions, difficulty, timeToComplete,
                        musclesWorked, gripWidth, isloatesMuscle, wristStress);
                }
                if (type == 3)
                {
                    inheritanceQuery = $"SELECT * FROM tblPullExercise WHERE fldExercise_Id " +
                        $"IN(SELECT fldExercise_Id from tblExercise_List WHERE fldMuscleGroup = {type}" +
                        $" AND fldExercise_Id= {id})";
                    secondaryDT = _adoHelper.GetDataTable(inheritanceQuery);
                    bool pullAngle = Convert.ToBoolean(secondaryDT.Rows[0]["fldPullAngle"]);
                    bool isAssisted = Convert.ToBoolean(secondaryDT.Rows[0]["fldIsAssisted"]);
                    int handPosition = int.Parse(secondaryDT.Rows[0]["fldHandPosition"].ToString());
                    exercise = new PullExercise(id, name, instructions, difficulty, timeToComplete,
                        musclesWorked, pullAngle, isAssisted, handPosition);
                }
                Vector exerciseVector = exercise.ExerciseVector;
                double score = Vector.GetDotProduct(exerciseVector, userVector);//חישוב ציון ההתאמה
                exercise.Score = score;
                allExercises.AddLast(exercise);//הוספה של התרגיל לרשימה של כלל התרגילים העובדים על שריר מסויים
            }
            //מיון הרשימה לפי ציון ההאתמה, ככה שבמקום הראשון
            //יהיה את התרגיל המתאים ביותר למשתמש, ובמקום האחרון יהיה את התרגיל שהכי פחות מתאים לו
            return SortExercisesByScore(allExercises);
        }

        // Sorts the provided exercises in descending order based on their score.
        public LinkedList<Exercise> SortExercisesByScore(LinkedList<Exercise> exercises)
        {
            List<Exercise> exerciseList = new List<Exercise>(exercises);
            exerciseList.Sort((ex1, ex2) => ex2.Score.CompareTo(ex1.Score));
            exercises.Clear();
            foreach (var exercise in exerciseList)
            {
                exercises.AddLast(exercise);
            }
            return exercises;
        }

        // Generates an HTML dropdown list of exercises for a given plan ID.
        public string GetExercisesList(int planId)
        {
            string query = "SELECT fldExercise_Id, fldExercise_Name FROM tblExercise_List";
            DataTable dt = AdoHelper.GetDataTable(file, query);
            string exercises = $"<select onchange = 'drawChart({planId})' id='exercise-list' name='exercises'>";
            foreach (DataRow row in dt.Rows)
            {
                int exerciseId = Convert.ToInt32(row["fldExercise_Id"]);
                string exerciseName = row["fldExercise_Name"].ToString();
                exercises += $"<option id='exercise{exerciseId}' value='{exerciseId}'>{exerciseName}</option>";
            }
            exercises += "</select>";
            return exercises;
        }

        // Retrieves a list of exercise formats containing the exercise ID and name.
        public List<ExerciseFormat> GetExerciseList()
        {
            string query = "SELECT fldExercise_Id, fldExercise_Name FROM tblExercise_List";
            DataTable dt = AdoHelper.GetDataTable(file, query);
            List<ExerciseFormat> exercises = new List<ExerciseFormat>();

            foreach (DataRow row in dt.Rows)
            {
                int exerciseId = Convert.ToInt32(row["fldExercise_Id"]);
                string exerciseName = row["fldExercise_Name"].ToString();

                // Create an exercise object with label and value
                exercises.Add(new ExerciseFormat(exerciseId, exerciseName));
            }
            return exercises;
        }

        // Determines and returns the next best exercise for a user based on their current exercise.
        public Exercise GetNextBestExercise(int userId, int exerciseId)
        {
            int muscleId = GetMusclesWorked(exerciseId).First.Value;
            string username = cu.GetUsernameFromId(userId);
            int userAge = cu.GetAge(username);
            Vector userVector = cur.GetUserVector(userId, userAge);
            string muscleName = mc.GetMuscleNameFromId(muscleId);
            LinkedList<Exercise> sortedExerciseList = AllExercises(muscleName, userVector);
            Exercise exercise = null;
            LinkedListNode<Exercise> currentNode = sortedExerciseList.First;

            // Traverse the list to find the node with the matching ID and return the next one.
            while (currentNode != null)
            {
                if (currentNode.Value.Id == exerciseId)
                {
                    // If the next node exists, set exercise to the next node's value.
                    if (currentNode.Next != null)
                    {
                        exercise = currentNode.Next.Value;
                    }
                    break; // Exit the loop once the match is found.
                }
                currentNode = currentNode.Next; // Move to the next node.
            }
            return exercise;
        }

        // Retrieves the name of an exercise given its ID.
        public string GetExerciseName(int exerciseId)
        {
            string query = $"SELECT fldExercise_Name FROM tblExercise_List WHERE fldExercise_Id = {exerciseId}";
            DataTable dt = _adoHelper.GetDataTable(query);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["fldExercise_Name"].ToString();
            return "";
        }

        // Asynchronously returns the next available exercise ID.
        public async Task<int> GetNextExerciseIdAsync()
        {
            string query = "SELECT ISNULL(MAX(fldExercise_Id), 0) + 1 AS NextId FROM tblExercise_List";
            DataTable dt = await _adoHelper.GetDataTableAsync(query); // Assuming AdoHelper supports async
            return dt.Rows.Count > 0 ? int.Parse(dt.Rows[0]["NextId"].ToString()) : 1;
        }

        // Asynchronously retrieves all exercises from the database.
        public async Task<DataSet> GetAllExercisesAsync()
        {
            string query = "SELECT * FROM tblExercise_List";
            return await _adoHelper.GetDataSetAsync(query); // Assuming AdoHelper supports async
        }

        // Asynchronously deletes an exercise based on its ID.
        public async Task<bool> DeleteExerciseAsync(int exerciseId)
        {
            string query = $"DELETE FROM tblExercise_List WHERE fldExercise_Id = {exerciseId}";
            return await _adoHelper.CheckInsertAsync(query) > 0; // Assuming AdoHelper supports async
        }

        // Asynchronously adds a new exercise to the database.
        public async Task<bool> AddExerciseAsync(string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            string query = $@"
                INSERT INTO tblExercise_List (fldExercise_Name, fldExercise_Desc, fldDifficulty, fldTime_To_Complete)
                VALUES ('{exerciseName}', '{exerciseDesc}', {difficulty}, {timeToComplete});
            ";
            return await _adoHelper.CheckInsertAsync(query) > 0; // Assuming AdoHelper supports async
        }

        // Asynchronously associates a muscle with an exercise.
        public async Task<bool> AddMuscleToExerciseAsync(int exerciseId, int muscleId)
        {
            string query = $@"
                INSERT INTO tblMuscles_Worked_In_Exercises (fldExercise_Id, fldMuscle_Id)
                VALUES ({exerciseId}, {muscleId});
            ";
            return await _adoHelper.CheckInsertAsync(query) > 0; // Assuming AdoHelper supports async
        }

        // Asynchronously edits an existing exercise's details.
        public async Task<bool> EditExerciseAsync(int exerciseId, string exerciseName, string exerciseDesc, double difficulty, int timeToComplete)
        {
            string query = $@"
                UPDATE tblExercise_List
                SET fldExercise_Name = '{exerciseName}',
                    fldExercise_Desc = '{exerciseDesc}',
                    fldDifficulty = {difficulty},
                    fldTime_To_Complete = {timeToComplete}
                WHERE fldExercise_Id = {exerciseId};
            ";
            return await _adoHelper.CheckInsertAsync(query) > 0; // Assuming AdoHelper supports async
        }

        // Asynchronously retrieves the list of muscle IDs worked by a specific exercise.
        public async Task<LinkedList<int>> GetMusclesWorkedAsync(int exerciseId)
        {
            LinkedList<int> musclesWorked = new LinkedList<int>();
            string query = $"SELECT fldMuscle_Id FROM tblMuscles_Worked_In_Exercises WHERE fldExercise_Id = {exerciseId}";
            DataTable dt = await _adoHelper.GetDataTableAsync(query); // Assuming AdoHelper supports async
            foreach (DataRow dr in dt.Rows)
            {
                int muscleId = int.Parse(dr["fldMuscle_Id"].ToString());
                musclesWorked.AddLast(muscleId);
            }
            return musclesWorked;
        }

        // Asynchronously retrieves all exercises for a given muscle name and sorts them based on user vector matching.
        public async Task<LinkedList<Exercise>> AllExercisesAsync(string muscleName, Vector userVector)
        {
            int muscleId = 0;
            string query = $"SELECT * FROM tblMuscles_List WHERE fldMuscle_Name = '{muscleName}'";
            DataTable dt = await _adoHelper.GetDataTableAsync(query); // Assuming AdoHelper supports async
            if (dt.Rows.Count != 0)
                muscleId = int.Parse((dt.Rows[0]["fldMuscle_Id"]).ToString());

            query = $"SELECT * FROM tblExercise_List WHERE fldExercise_Id IN (SELECT fldExercise_Id FROM tblMuscles_Worked_In_Exercises WHERE fldMuscle_Id = {muscleId}) ";
            DataTable exerciseDt = await _adoHelper.GetDataTableAsync(query); // Assuming AdoHelper supports async

            LinkedList<Exercise> allExercises = new LinkedList<Exercise>();
            foreach (DataRow row in exerciseDt.Rows)
            {
                int id = int.Parse(row["fldExercise_Id"].ToString());
                string name = row["fldExercise_Name"].ToString();
                string instructions = row["fldExercise_Desc"].ToString();
                double difficulty = double.Parse(row["fldDifficulty"].ToString());
                int timeToComplete = int.Parse(row["fldTime_To_Complete"].ToString());
                int type = int.Parse(row["fldMuscleGroup"].ToString());
                LinkedList<int> musclesWorked = new LinkedList<int>();
                string inheritanceQuery = "";
                Exercise exercise = null;
                DataTable secondaryDT;
                if (type == 0)
                {
                    inheritanceQuery = $"SELECT * FROM tblLeg_Exercises WHERE fldExercise_Id IN(SELECT fldExercise_Id from tblExercise_List WHERE fldMuscleGroup = {type} AND fldExercise_Id= {id})";
                    secondaryDT = await _adoHelper.GetDataTableAsync(inheritanceQuery); // Assuming AdoHelper supports async
                    double kneeDamage = double.Parse(secondaryDT.Rows[0]["fldKnee_Damage"].ToString());
                    bool isSquat = Convert.ToBoolean(secondaryDT.Rows[0]["fldIs_Squat_Based"]);
                    exercise = new LegExercise(id, name, instructions, difficulty, timeToComplete, musclesWorked, kneeDamage, isSquat);
                }
                if (type == 1)
                {
                    inheritanceQuery = $"SELECT * FROM tblPush_Exercises WHERE fldExercise_Id IN(SELECT fldExercise_Id from tblExercise_List WHERE fldMuscleGroup = {type} AND fldExercise_Id= {id})";
                    secondaryDT = await _adoHelper.GetDataTableAsync(inheritanceQuery); // Assuming AdoHelper supports async
                    double elbowDamage = double.Parse(secondaryDT.Rows[0]["fldElbow_Damage"].ToString());
                    double explosiveness = double.Parse(secondaryDT.Rows[0]["fldExplosiveness"].ToString());
                    exercise = new PushExercise(id, name, instructions, difficulty, timeToComplete, musclesWorked, elbowDamage, explosiveness);
                }
                if (type == 2)
                {
                    inheritanceQuery = $"SELECT * FROM tblArmsExercise WHERE fldExercise_Id IN(SELECT fldExercise_Id from tblExercise_List WHERE fldMuscleGroup = {type} AND fldExercise_Id= {id})";
                    secondaryDT = await _adoHelper.GetDataTableAsync(inheritanceQuery); // Assuming AdoHelper supports async
                    bool isloatesMuscle = Convert.ToBoolean(secondaryDT.Rows[0]["fldIsolatesMuscle"]);
                    double wristStress = double.Parse(secondaryDT.Rows[0]["fldWristStress"].ToString());
                    int gripWidth = int.Parse(secondaryDT.Rows[0]["fldGripWidth"].ToString());
                    exercise = new ArmExercise(id, name, instructions, difficulty, timeToComplete, musclesWorked, gripWidth, isloatesMuscle, wristStress);
                }
                if (type == 3)
                {
                    inheritanceQuery = $"SELECT * FROM tblPullExercise WHERE fldExercise_Id IN(SELECT fldExercise_Id from tblExercise_List WHERE fldMuscleGroup = {type} AND fldExercise_Id= {id})";
                    secondaryDT = await _adoHelper.GetDataTableAsync(inheritanceQuery); // Assuming AdoHelper supports async
                    bool pullAngle = Convert.ToBoolean(secondaryDT.Rows[0]["fldPullAngle"]);
                    bool isAssisted = Convert.ToBoolean(secondaryDT.Rows[0]["fldIsAssisted"]);
                    int handPosition = int.Parse(secondaryDT.Rows[0]["fldHandPosition"].ToString());
                    exercise = new PullExercise(id, name, instructions, difficulty, timeToComplete, musclesWorked, pullAngle, isAssisted, handPosition);
                }
                Vector exerciseVector = exercise.ExerciseVector;
                double score = Vector.GetDotProduct(exerciseVector, userVector);
                exercise.Score = score;
                allExercises.AddLast(exercise);
            }
            return SortExercisesByScore(allExercises);
        }

        // Asynchronously sorts the exercises based on their score in descending order.
        public async Task<LinkedList<Exercise>> SortExercisesByScoreAsync(LinkedList<Exercise> exercises)
        {
            List<Exercise> exerciseList = new List<Exercise>(exercises);
            exerciseList.Sort((ex1, ex2) => ex2.Score.CompareTo(ex1.Score));
            exercises.Clear();
            foreach (var exercise in exerciseList)
            {
                exercises.AddLast(exercise);
            }
            return exercises;
        }

        // Asynchronously generates an HTML dropdown list of exercises for a given plan ID.
        public async Task<string> GetExercisesListAsync(int planId)
        {
            string query = "SELECT fldExercise_Id, fldExercise_Name FROM tblExercise_List";
            DataTable dt = await _adoHelper.GetDataTableAsync(query); // Assuming AdoHelper supports async
            string exercises = $"<select onchange = 'drawChart({planId})' id='exercise-list' name='exercises'>";
            foreach (DataRow row in dt.Rows)
            {
                int exerciseId = Convert.ToInt32(row["fldExercise_Id"]);
                string exerciseName = row["fldExercise_Name"].ToString();
                exercises += $"<option id='exercise{exerciseId}' value='{exerciseId}'>{exerciseName}</option>";
            }
            exercises += "</select>";
            return exercises;
        }

        // Asynchronously determines and returns the next best exercise for a user based on their current exercise.
        public async Task<Exercise> GetNextBestExerciseAsync(int userId, int exerciseId)
        {
            int muscleId = (await GetMusclesWorkedAsync(exerciseId)).First.Value;
            string username = cu.GetUsernameFromId(userId);
            int userAge = cu.GetAge(username);
            Vector userVector = cur.GetUserVector(userId, userAge);
            string muscleName = mc.GetMuscleNameFromId(muscleId);
            LinkedList<Exercise> sortedExerciseList = await AllExercisesAsync(muscleName, userVector);
            Exercise exercise = null;
            LinkedListNode<Exercise> currentNode = sortedExerciseList.First;

            // Traverse the list to find the node with the matching ID and return the next one.
            while (currentNode != null)
            {
                if (currentNode.Value.Id == exerciseId)
                {
                    // If the next node exists, set exercise to the next node's value.
                    if (currentNode.Next != null)
                    {
                        exercise = currentNode.Next.Value;
                    }
                    break; // Exit the loop once the match is found.
                }
                currentNode = currentNode.Next; // Move to the next node.
            }
            return exercise;
        }

        // Asynchronously retrieves the name of an exercise given its ID.
        public async Task<string> GetExerciseNameAsync(int exerciseId)
        {
            string query = $"SELECT fldExercise_Name FROM tblExercise_List WHERE fldExercise_Id = {exerciseId}";
            DataTable dt = await _adoHelper.GetDataTableAsync(query); // Assuming AdoHelper supports async
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["fldExercise_Name"].ToString();
            return "";
        }
    }
}
