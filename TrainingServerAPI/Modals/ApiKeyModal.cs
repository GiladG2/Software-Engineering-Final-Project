using ControllersProject.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrainingServerAPI.Modals;

namespace TrainingServerAPI.Controllers
{
    // Rename class to ApiKeyValidationController to avoid conflicts with the IResult interface name.
    public class ApiKeyModal : IApiKeyValidation
    {
        public string apiKey { get=> apiKey; set => apiKey = value; }
        public bool IsValidApiKey(string apiKey)
        {
            string query = $"SELECT * FROM tblUsers WHERE fldApiKey = '{apiKey}'";
            DataTable dt = AdoHelper.GetDataTable("DB.mdf",query);
            return dt.Rows.Count > 0;
        }
      
    }
}
