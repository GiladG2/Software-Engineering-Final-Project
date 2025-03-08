using ControllersProject.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Controller
{
    public class User_Requests_Controller
    {
        private Modal_User_Requests mUserRequests = new Modal_User_Requests();
       
        public bool AddUserRequest(int userid, int duration, int daysAWeek, int exeperience, int typeOfPlan, int[] injuryList)
        {
            return mUserRequests.AddUserRequests(userid, duration, daysAWeek, exeperience, typeOfPlan, injuryList);
        }
        public bool EditUserRequest(int userid, int duration, int daysAWeek, int exeperience, int typeOfPlan, int[] injuryList)
        {
            return mUserRequests.UpdateUserRequest(userid, duration, daysAWeek, exeperience, typeOfPlan, injuryList);
        }
        public int GetDuration(int userid)
        {
            return mUserRequests.GetDuration(userid);
        }
        public int GetDaysAWeek(int userid)
        {
            return mUserRequests.GetDaysAweek(userid);
        }
        public int GetTypeOfTraining(int userid)
        {
            return mUserRequests.GetDaysAweek(userid);
        }
        public int GetExperience(int userid)
        {  
            return mUserRequests.GetExperience(userid);
        }
        public Vector GetUserVector(int userid, int userAge)
        {
            return mUserRequests.UserVector(userid, userAge);
        }
    }
}
