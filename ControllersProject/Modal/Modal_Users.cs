using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllersProject.DAL;

namespace ControllersProject.Modal
{
    internal class Modal_Users
    {
        private const string file = "DB.mdf";

        public bool UpdateData(string query)
        {
            if (AdoHelper.CheckInsert(file, query) > 0)
                return true;
            return false;
        }

        public DataTable SelectData(string query)
        {
            return AdoHelper.GetDataTable(file, query);
        }

        public DataSet GetDataSet(string query)
        {
            return AdoHelper.GetDataSet(file, query);
        }

        // Get
        public int GetUserId(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return int.Parse((dt.Rows[0]["user_id"]).ToString());
            return -1;
        }

        public int GetAccessKey(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return int.Parse((dt.Rows[0]["fldAccess"]).ToString());
            return -1;
        }

        public string GetFirstName(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return (dt.Rows[0]["fldFirstName"]).ToString();
            return "";
        }

        public string GetPassword(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return (dt.Rows[0]["fldPassword"]).ToString();
            return "";
        }

        public int GetGender(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                if (dt.Rows[0]["fldGender"].ToString() == "True")
                    return 1;
            return 0;
        }

        public string GetGenderName(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                if (dt.Rows[0]["fldGender"].ToString() == "True")
                    return "male";
            return "female";
        }

        public string GetPhonenumber(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return (dt.Rows[0]["fldPhone"].ToString());
            return "";
        }

        public string GetDate(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return dt.Rows[0]["fldDate"].ToString();
            return "";
        }

        public int GetAge(string username)
        {
            string birthDateString = GetDate(username);
            if (string.IsNullOrEmpty(birthDateString))
            {
                // If no birthdate is found, return a default age (or handle as appropriate)
                return -1;
            }
            DateTime birthDate;
            if (!DateTime.TryParse(birthDateString, out birthDate))
            {
                // If birthdate can't be parsed, return a default age (or handle as appropriate)
                return -1;
            }
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
                age--;
            return age;
        }

        public string GetUsernameById(int user_Id)
        {
            string query = $"SELECT fldUsername FROM tblUsers WHERE user_id = {user_Id}";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return dt.Rows[0]["fldUsername"].ToString();
            return "";
        }

        public bool IsExist(int userId)
        {
            string query = $"SELECT * FROM tblUsers WHERE user_id = '{userId}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return true;
            return false;
        }

        public int GetGoal(string username)
        {
            int goals = 0;
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["fldGoals"].ToString() == "True")
                    goals = 1;
            }
            return goals;
        }

        public string GetEmail(string username)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return dt.Rows[0]["fldEmail"].ToString();
            return "";
        }

        // Log in
        public bool Testada(string username, string password)
        {
            using (SqlConnection connection = AdoHelper.ConnectToDb(file))
            {
                using (SqlCommand command = new SqlCommand("spLog_In", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@username", username));
                    command.Parameters.Add(new SqlParameter("@password", password));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                    return false;
                }
            }
        }

        // Sign in
        public bool AddData(string username, string password, string firstname, string phonenumber, int gender, string date, int goal, int access, string email)
        {
            if(Testada(username, password)) {
                return false;
            }
            string sql = $"INSERT INTO tblUsers (fldUsername, fldPassword, fldFirstname, fldPhone, fldGender, fldDate, fldGoals, fldAccess, fldEmail) ";
            sql += $"VALUES ('{username}', '{password}', '{firstname}', '{phonenumber}', '{gender}', '{date}', '{goal}', '{access}', '{email}')";
            return AdoHelper.CheckInsert(file, sql) > 0;
        }

        public bool IsTaken(string username)
        {
            string query = "SELECT * FROM tblUsers WHERE fldUsername=@username";
            using (SqlConnection connection = AdoHelper.ConnectToDb(file))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@username", username));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return reader.HasRows;
                }
            }
        }

        // Delete
        public bool DeleteUser(string username)
        {
            string query = $"DELETE FROM tblUsers WHERE fldUsername=@username";
            return  AdoHelper.CheckInsert(file,query) > 0;  
        }

        // Edit Profile
        public bool UpdateDataForEdit(string password, string date, string phonenumber, string firstname, int gender, int goal, string username)
        {
            string query = $"UPDATE tblUsers SET fldPassword = '{password}', fldDate='{date}', fldPhone='{phonenumber}', fldFirstname='{firstname}', fldGender='{gender}', fldGoals='{goal}' WHERE fldUsername = '{username}'";
            return AdoHelper.CheckInsert(file, query) > 0;
        }

        public bool UpdateUser(string username, string firstName, string password, string phonenumber, string email)
        {
            string query = $"UPDATE tblUsers SET fldPassword = '{password}', fldEmail = '{email}', fldPhone='{phonenumber}' , fldFirstname='{firstName}'  WHERE fldUsername = '{username}'";
            return AdoHelper.CheckInsert(file, query) > 0;
        }
        public bool EditPass(string newPassword, int userId)
        {
            string query = $"UPDATE tblUsers SET fldPassword = '{newPassword}' WHERE user_id = {userId}";
            return AdoHelper.CheckInsert(file, query) > 0;
        }

        // Admin

        // User requests - training plans
        public bool AddUserRequests(int userId, int duration, int daysAWeek, int experience, int typeOfPlan, int[] injuryList)
        {
            string sql = $"INSERT INTO tblUser_Requests (fldUser_Id, fldDuration, fldExperience, fldType_Of_Training, fldDays_A_Week) ";
            sql += $"VALUES ('{userId}', '{duration}', '{experience}', '{typeOfPlan}', '{daysAWeek}')";
            bool checkInsert = AdoHelper.CheckInsert(file, sql) > 0;
            if (!checkInsert)
                return false;
            for (int i = 0; i < injuryList.Length; i++)
            {
                sql = $"INSERT INTO tblInjury_Record(fldUser_Id, fldMuscle_Id) VALUES({userId}, {injuryList[i]})";
                if (!(AdoHelper.CheckInsert(file, sql) > 0))
                    return false;
            }
            return true;
        }

        public bool AlreadyFiledRequest(int user_id)
        {
            string query = $"SELECT * FROM tblUser_Requests WHERE fldUser_Id = {user_id}";
            DataTable dt = SelectData(query);
            if (dt.Rows.Count != 0)
                return true;
            return false;
        }

        public string GenerateAPIKey()
        {
            string apiKey = Guid.NewGuid().ToString();
            /*  while (CheckIsAPIKeyTaken(apiKey))
                  apiKey = Guid.NewGuid().ToString(); */
            return apiKey;
        }

        



    }
}
