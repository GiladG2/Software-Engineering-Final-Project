using PlanDataWebService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanDataWebService.Controller
{
    public class Controller_Users
    {

        Modal_Users mu = new Modal_Users();
        public string GenerateAPIKey()
        {
            return mu.GenerateAPIKey();
        }
        public bool CheckIsUsernameTaken(string username)
        {
            return mu.CheckIsUsernameTaken(username);
        }
        public bool AddUser(string username, string password)
        {
            return mu.AddUser(username, password);
        }
        public int GetUserIdByUsername(string username)
        {
            return mu.GetUserIdByUsername(username);
        }
        public bool IsValidUser(string username, string password)
        {
            return mu.IsValidUser(username, password);
        }

    }
}