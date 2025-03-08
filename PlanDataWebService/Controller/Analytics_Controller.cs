using ControllersProject.DAL;
using Newtonsoft.Json;
using PlanDataWebService.Modal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PlanDataWebService.Controller
{
    public class Analytics_Controller
    {
        private Modal_Analytics ma = new Modal_Analytics();

        public bool AddInsert(int user_Id)
        {
            return ma.AddInsert(user_Id);
        }
        public bool AddUpdate(int user_Id)
        {
            return ma.AddUpdate(user_Id);
        }
        public bool AddDelete(int user_Id)
        {
            return ma.AddDelete(user_Id);
        }
        public AnalyticsData GetAnalyticsForUser(int userId)
        {
            return ma.GetAnalyticsForUser(userId);
        }
        public string GetInsertDataForPeriod(int userId, int period)
        {
            return ma.GetOperationDataForPeriod(userId, period, 0); // 0 is for Insert operation
        }

        public string GetUpdateDataForPeriod(int userId, int period)
        {
            return ma.GetOperationDataForPeriod(userId, period, 1); // 1 is for Update operation
        }

        public string GetDeleteDataForPeriod(int userId, int period)
        {
            return ma.GetOperationDataForPeriod(userId, period, 2); // 2 is for Delete operation
        }
        public string GetGraphData(int userId,int period, int operationId)
        {
            return ma.GetOperationDataForPeriod(userId,period, operationId);
        }
        // Helper method to fetch operation data by period and operation type
       
    }
}