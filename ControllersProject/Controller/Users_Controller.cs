using ControllersProject.Modal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Controller
{
    public class Users_Controller
    {
        private Modal_Users modeldata;
        public Users_Controller() {
            modeldata = new Modal_Users();
        }
        public DataTable GetData(string query)
        {
            return modeldata.SelectData(query);
        }
        public DataSet GetDataSet(string query)
        {
            return modeldata.GetDataSet(query);
        }
        public bool IsExist(int userid)
        {
            return modeldata.IsExist(userid);
        }
        public bool EditPass(int userId, string newPass)
        {
            return modeldata.EditPass(newPass, userId);
        }
        //Get
        public int GetAccessKey(string username)
        {
            return modeldata.GetAccessKey(username);
        }
        public string GetFirstName(string username)
        {
            return modeldata.GetFirstName(username);
        }
        public string GetPassword(string username)
        {
            return modeldata.GetPassword(username);
        }
        public string GetPhonenumber(string username)
        {
            return modeldata.GetPhonenumber(username);
        }
        public int GetGender(string username)
        {
            return modeldata.GetGender(username);
        }
        public string GetGenderName(string username)
        {
            return modeldata.GetGenderName(username);
        }
        public string GetDate(string username)
        {
            return modeldata.GetDate(username);
        }
        public int GetAge(string username)
        {
            return modeldata.GetAge(username);
        }
        public string GetUsernameFromId(int id)
        {
            return modeldata.GetUsernameById(id);
        }
        public int GetGoal(string username)
        {
            return modeldata.GetGoal(username);
        }
        public int GetUserId(string username)
        {
            return modeldata.GetUserId(username);
        }
       
        public string GetEmail(string username)
        {
            return modeldata.GetEmail(username);
        }
       

        //Log In:
        public bool TestData(string username, string password)
        {
            return modeldata.Testada(username, password);
        }

        //Sign in:
        public bool AddData(string username, string password, string firstname, string phonenumber, int gender, string date, int goal, int access, string email)
        {
            return modeldata.AddData(username, password, firstname, phonenumber, gender, date, goal, access, email);
        }
        public bool IsTaken(string username)
        {
            return modeldata.IsTaken(username);
        }
        //Delete
        public bool DeleteUser(string username)
        {
            return modeldata.DeleteUser(username);
        }
        //Edit Profile
        public bool UpdateDataForEdit(string username, string password, string date, string phonenumber, string firstname, int gender, int goal)
        {
            return modeldata.UpdateDataForEdit(password, date, phonenumber, firstname, gender, goal, username);
        }
        public bool EditUser(string username,string password, string phonenumber, string firstname,string email)
        {
            return modeldata.UpdateUser(username, firstname, password, phonenumber, email);
        }
        //User Request
        public bool AddUserRequest(int userId, int duration, int daysAWeek, int experience, int typeOfPlan, int[] injuryList)
        {
            return modeldata.AddUserRequests(userId, duration, daysAWeek, experience, typeOfPlan, injuryList);
        }
        public bool AlreadyFiledRequest(int userid)
        {
            return modeldata.AlreadyFiledRequest(userid);
        }
        public string GenerateAPIKey()
        {
            return modeldata.GenerateAPIKey();
        }
        //Download link
        /*
        public string GetPlansToXml()
        {
            return modeldata.GetPlansToXml();
        }
        public string GetAllUsers()
        {
            return modeldata.GetAllUsers();
        }*/
    }
}
