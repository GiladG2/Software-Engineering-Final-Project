using ControllersProject.Controller;
using System;
using System.Data;
using ControllersProject.DAL;
using System.Text;

namespace ControllersProject.Modal
{
    internal class Modal_Muscles
    {
        private readonly AdoHelper _adoHelper = new AdoHelper();
        private Users_Controller cu = new Users_Controller();
        public Modal_Muscles(string dbName)
        {
            _adoHelper = new AdoHelper(dbName);
        }
        public Modal_Muscles()
        {
            _adoHelper = new AdoHelper();
        }
        
        //פעולה המחזירה את מס הזהות האחרון במסד הנתונים ומוסיפה לו אחד
        private int GetNextMuscleId()
        {
            string query = "SELECT ISNULL(MAX(fldMuscle_Id), 0) + 1 AS NextId FROM tblMuscles_List";
            DataTable dt = _adoHelper.GetDataTable(query);
            return dt.Rows.Count > 0 ? int.Parse(dt.Rows[0]["NextId"].ToString()) : 1;
        }

        //פעולה המחזירה שם של שריר לפי מס הזהות שלו
        public string GetMuscleNameFromId(int muscleId)
        {
            string query = $"SELECT fldMuscle_Name FROM tblMuscles_List WHERE fldMuscle_Id = {muscleId}";
            DataTable dt = cu.GetData(query);
            if (dt.Rows.Count != 0)
                return (dt.Rows[0]["fldMuscle_Name"]).ToString();
            return "";
        }
        public DataSet GetAllMuscles()
        {
            string query = "SELECT * FROM tblMuscles_List";
            return _adoHelper.GetDataSet(query);
        }
        //פעולה המוסיפה שריר למסד הנתונים
        public bool AddMuscle(string muscleName, string muscleDescription, int muscleGroupId)
        {
            // Insert into tblMuscles_List
            string query = $"INSERT INTO tblMuscles_List (fldMuscle_Name, fldDescription) VALUES ('{muscleName}', '{muscleDescription}')";
            bool isInserted = _adoHelper.CheckInsert(query) > 0;

            if (isInserted)
            {
                // Get the newly inserted muscle ID
                int muscleId = GetNextMuscleId() - 1;

                // Insert into tblMuscleGroupMuscles
                string groupQuery = $"INSERT INTO tblMuscleGroupMuscles (fldMuscle_Group_Id, fldMuscle_Id) VALUES ({muscleGroupId}, {muscleId})";
                isInserted &= _adoHelper.CheckInsert(groupQuery) > 0;
            }

            return isInserted;
        }
        //פעולה העורכת שריר במסד הנתונים
        public bool EditMuscle(int muscleId, string muscleName)
        {
            string query = $@"
                UPDATE tblMuscles_List
                SET fldMuscle_Name = '{muscleName}'
                WHERE fldMuscle_Id = {muscleId};
            ";
            return _adoHelper.CheckInsert(query) > 0;
        }

        public bool DeleteMuscle(int muscleId)
        {
            string query = $@"
                DELETE FROM tblMuscles_Worked_In_Exercises
                WHERE fldMuscle_Id = {muscleId};
                
                DELETE FROM tblMuscleGroupMuscles
                WHERE fldMuscle_Id = {muscleId};
                
                DELETE FROM tblMuscles_List
                WHERE fldMuscle_Id = {muscleId};
            ";
            return _adoHelper.CheckInsert(query) > 0;
        }

        //פעולה המחזירה רשימה נפתחת של כל השרירים במסד הנתונים
        public string GetAllMusclesForDropdown()
        {
            try
            {
                // Query to fetch the muscles
                string query = "SELECT fldMuscle_Id, fldMuscle_Name FROM tblMuscles_List";
                string select = $"<select id='muscle' name='muscle'>";
                select += "<option value=''>Select Muscle</option>";
                // Get the dataset from the helper method
                DataSet musclesDataSet = _adoHelper.GetDataSet(query);

                if (musclesDataSet != null && musclesDataSet.Tables.Count > 0)
                {
                    StringBuilder optionsBuilder = new StringBuilder();

                    // Iterate over the rows in the DataSet and build the options
                    foreach (DataRow muscle in musclesDataSet.Tables[0].Rows)
                    {
                        string muscleId = muscle["fldMuscle_Id"].ToString();
                        string muscleName = muscle["fldMuscle_Name"].ToString();

                        // Add each muscle as an <option> to the StringBuilder
                        select += $"<option value='{muscleId}'>{muscleName}</option>";
                    }
                    select += "</select>";
                    // Return the built options as a string
                    return select;
                }

                return string.Empty; // Return an empty string if no data is found
            }
            catch (Exception ex)
            {
                // Handle any exceptions (log or display an error message)
                Console.WriteLine("Error fetching muscles for dropdown: " + ex.Message);
                return string.Empty;
            }
        }

    }
}
