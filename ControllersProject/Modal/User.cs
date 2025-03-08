using ControllersProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControllersProject.Modal
{
    public class User
    {
        private int userId;
        private string username;
        private string password;
        private bool gender;
        private string date;
        private string phone;
        private bool goals;
        private string firstName;
        private int accessKey;
        private bool hasPlan;
        private string email;
        private int[] injuryrecord;
        private Vector userVector;

        Users_Controller cu = new Users_Controller();

        public User(int userId) {
          this.UserId = userId;
          Plan_Controller pc = new Plan_Controller();
         this.Username = cu.GetUsernameFromId(userId);
         this.Gender = cu.GetGender(Username) == 1? true : false;
            this.Date = cu.GetDate(Username);
            this.Goals = cu.GetGoal(Username) == 1? true : false;
           this.FirstName = cu.GetFirstName(Username);
            this.AccessKey = cu.GetAccessKey(Username);
            this.hasPlan = pc.HasAPlan(userId);
        }
        public User(string username)
        {
            this.username = username;
            this.password = cu.GetPassword(Username);
            this.gender = cu.GetGender(Username) == 1 ? true : false;
            this.date = cu.GetDate(Username);
            this.goals = cu.GetGoal(Username) == 1 ? true : false;
            this.firstName = cu.GetFirstName(Username);
            this.accessKey = cu.GetAccessKey(Username);
            this.email = cu.GetEmail(Username);
            this.Phone = cu.GetPhonenumber(Username);
        }
        public int UserId { get => userId; set => userId = value; }
        public string Username { get => username; set => username = value; }
        public bool Gender { get => gender; set => gender = value; }
        public string Date { get => date; set => date = value; }
        public bool Goals { get => goals; set => goals = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public int AccessKey { get => accessKey; set => accessKey = value; }
        public bool HasPlan { get => hasPlan; set => hasPlan = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Password { get => password; set => password = value; }
    }
}