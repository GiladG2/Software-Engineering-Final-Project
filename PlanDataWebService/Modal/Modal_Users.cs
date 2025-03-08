using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ControllersProject.DAL;
namespace PlanDataWebService.Modal
{
    public class Modal_Users
    {
        private static string dbName = "ServerDB.mdf";
        private AdoHelper adoHelper = new AdoHelper(dbName);
        // Generate an API Key
        public string GenerateAPIKey()
        {
            string apiKey = Guid.NewGuid().ToString();
          /*  while (CheckIsAPIKeyTaken(apiKey))
                apiKey = Guid.NewGuid().ToString(); */
            return apiKey;
        }
        public int GetUserIdByUsername(string username)
        {
            string query = $"SELECT fldUser_Id FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = AdoHelper.GetDataTable(dbName, query);
            if (dt.Rows.Count > 0)
                return int.Parse(dt.Rows[0]["fldUser_Id"].ToString());
            return -1;
        }
        // Add a new user to the database
        public bool AddUser(string username, string password)
        {
            if (CheckIsUsernameTaken(username))
            {
                return false; // Username already exists
            }

            // Insert the new user into the database
            string query = $"INSERT INTO tblUsers (fldUsername, fldPassword) VALUES ('{username}', '{password}')";
            if(AdoHelper.CheckInsert(dbName,query) > 0)
            {
                int userId = GetUserIdByUsername(username);
                query = $"Insert into tblAnalytics (fldUser_Id,fldInsert,fldUpdate,fldDelete) VALUES ({userId},0,0,0)";
                return AdoHelper.CheckInsert(dbName, query) > 0;
            }
            return false;
        }

        // Check if a username is already taken
        public bool CheckIsUsernameTaken(string username)
        {
            // Query to check the existence of the username
            string query = $"SELECT COUNT(*) FROM tblUsers WHERE fldUsername = '{username}'";
            DataTable dt = AdoHelper.GetDataTable(dbName, query);

            // If the query returns a count greater than zero, the username is taken
            return dt.Rows.Count > 0 && int.Parse(dt.Rows[0][0].ToString()) > 0;
        }

        // Check if an API key is already in use
        public bool CheckIsAPIKeyTaken(string apiKey)
        {  
            // Query to check the existence of the API key
            string query = $"SELECT COUNT(*) FROM tblUsers WHERE fldAPIKey = '{apiKey}'";
            DataTable dt = AdoHelper.GetDataTable(dbName,query);

            // If the query returns a count greater than zero, the API key is taken
            return dt.Rows.Count > 0 && int.Parse(dt.Rows[0][0].ToString()) > 0;
        }
        public bool IsValidUser(string username, string password)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldUsername = '{username}' AND fldPassword = '{password}'";
            return adoHelper.GetDataTable(query).Rows.Count > 0 ;
        }
    }
}