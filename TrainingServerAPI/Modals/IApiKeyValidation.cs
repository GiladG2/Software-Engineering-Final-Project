using ControllersProject.DAL;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace TrainingServerAPI.Modals
{
    public interface IApiKeyValidation
    {

        // public string? apiKey { get => apiKey; set => apiKey = value; }
        public bool IsValidApiKey(string key);
        
    }
}
