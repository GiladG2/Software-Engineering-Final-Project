using ControllersProject.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllersProject.DAL;

namespace ControllersProject.Modal
{
    internal class Modal_User_Requests
    {
        Users_Controller cu = new Users_Controller(); // Use Users_Controller instead of Modal_Users
        string file = "DB.mdf"; // Name of the database file

        public bool AddUserRequests(int userId, int duration, int daysAWeek, int experience, int typeOfPlan, int[] injuryList)
        {
            // Construct the SQL query using string interpolation with single quotes around each value
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

        public bool UpdateUserRequest(int userId, int duration, int daysAWeek, int experience, int typeOfPlan, int[] injuryList)
        {
            string str = $"UPDATE tblUser_Requests SET fldDuration = '{duration}', fldExperience='{experience}', fldType_Of_Training='{typeOfPlan}', fldDays_A_Week='{daysAWeek}' WHERE fldUser_Id = '{userId}' ";
            if (!(AdoHelper.CheckInsert(file, str) > 0))
                return false;

            str = $"DELETE FROM tblInjury_Record WHERE fldUser_Id = {userId}";
            AdoHelper.CheckInsert(file, str);

            for (int i = 0; i < injuryList.Length; i++)
            {
                str = $"INSERT INTO tblInjury_Record(fldUser_Id, fldMuscle_Id) VALUES({userId}, {injuryList[i]})";
                if (!(AdoHelper.CheckInsert(file, str) > 0))
                    return false;
            }
            return true;
        }

        public int GetDaysAweek(int user_id)
        {
            string query = $"SELECT * FROM tblUser_Requests WHERE fldUser_Id = '{user_id}'";
            DataTable dt = AdoHelper.GetDataTable(file, query);
            if (dt.Rows.Count != 0)
                return int.Parse((dt.Rows[0]["fldDays_A_Week"]).ToString());
            return -1;
        }

        public int GetDuration(int user_id)
        {
            string query = $"SELECT * FROM tblUser_Requests WHERE fldUser_Id = '{user_id}'";
            DataTable dt = AdoHelper.GetDataTable(file, query);
            if (dt.Rows.Count != 0)
                return int.Parse((dt.Rows[0]["fldDuration"]).ToString());
            return -1;
        }

        public int GetTypeOfTraining(int user_id)
        {
            string query = $"SELECT * FROM tblUser_Requests WHERE fldUser_Id = '{user_id}'";
            DataTable dt = AdoHelper.GetDataTable(file, query);
            if (dt.Rows.Count != 0)
                return int.Parse((dt.Rows[0]["fldType_Of_Training"]).ToString());
            return -1;
        }

        public int GetExperience(int user_id)
        {
            string query = $"SELECT * FROM tblUser_Requests WHERE fldUser_Id = '{user_id}'";
            DataTable dt = AdoHelper.GetDataTable(file, query);
            if (dt.Rows.Count != 0)
                return int.Parse((dt.Rows[0]["fldExperience"]).ToString());
            return -1;
        }

        public Vector UserVector(int user_id, int userAge)
        {
            int x = GetDuration(user_id) / 2;
            int y = GetExperience(user_id);
            int z = 0;
            double constant = userAge > 55 ? 55 / userAge : -1 * userAge / 55;
            return new Vector(constant + x, constant + y, constant + z);
        }
    }
}
