using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ControllersProject.DAL;
using Newtonsoft.Json;

namespace PlanDataWebService.Modal
{
    public class AnalyticsData
    {
        public int Inserts { get; set; } // 0
        public int Updates { get; set; } // 1
        public int Deletes { get; set; } // 2
    }

    internal class Modal_Analytics
    {
        private static string file = "ServerDB.mdf";
        private string dbName = "ServerDB.mdf";
        private AdoHelper adoHelper = new AdoHelper(file);

        public AnalyticsData GetAnalyticsForUser(int userId)
        {
            AnalyticsData data = new AnalyticsData();
            string query = $"SELECT fldInsert, fldUpdate, fldDelete FROM tblAnalytics WHERE fldUser_Id = {userId}";
            DataTable dt = adoHelper.GetDataTable(query);
            data.Inserts = Convert.ToInt32(dt.Rows[0]["fldInsert"].ToString());
            data.Updates = Convert.ToInt32(dt.Rows[0]["fldUpdate"]);
            data.Deletes = Convert.ToInt32(dt.Rows[0]["fldDelete"]);
            return data;
        }

        // Increment the 'fldUpdate' counter for a specific user and log the action in tblAnalyticsTracker
        public bool AddUpdate(int userId)
        {
            // Update the 'tblAnalytics' table
            string updateQuery = $"UPDATE tblAnalytics SET fldUpdate = fldUpdate + 1 WHERE fldUser_Id = {userId}";
            bool result = adoHelper.CheckInsert(updateQuery) > 0;

            if (result)
            {
                // Get the Analytics ID for the user
                string selectAnalyticsIdQuery = $"SELECT fldAnalytics_Id FROM tblAnalytics WHERE fldUser_Id = {userId}";
                DataTable dt = adoHelper.GetDataTable(selectAnalyticsIdQuery);
                int analyticsId = Convert.ToInt32(dt.Rows[0]["fldAnalytics_Id"]);

                // Insert into 'tblAnalyticsTracker' table
                string insertTrackerQuery = $"INSERT INTO tblAnalyticsTracker (fldAnalytics_Id, fldOperation_Id, fldOperation_Date) " +
                                            $"VALUES ({analyticsId}, 1, CAST(GETDATE() AS DATE))"; // 1 is for 'update' operation
                adoHelper.CheckInsert(insertTrackerQuery);
            }

            return result;
        }

        // Increment the 'fldInsert' counter for a specific user and log the action in tblAnalyticsTracker
        public bool AddInsert(int userId)
        {
            // Update the 'tblAnalytics' table
            string updateQuery = $"UPDATE tblAnalytics SET fldInsert = fldInsert + 1 WHERE fldUser_Id = {userId}";
            bool result = adoHelper.CheckInsert(updateQuery) > 0;

            if (result)
            {
                // Get the Analytics ID for the user
                string selectAnalyticsIdQuery = $"SELECT fldAnalytics_Id FROM tblAnalytics WHERE fldUser_Id = {userId}";
                DataTable dt = adoHelper.GetDataTable(selectAnalyticsIdQuery);
                int analyticsId = Convert.ToInt32(dt.Rows[0]["fldAnalytics_Id"]);

                // Insert into 'tblAnalyticsTracker' table
                string insertTrackerQuery = $"INSERT INTO tblAnalyticsTracker (fldAnalytics_Id, fldOperation_Id, fldOperation_Date) " +
                                            $"VALUES ({analyticsId}, 0, CAST(GETDATE() AS DATE))"; // 0 is for 'insert' operation
                adoHelper.CheckInsert(insertTrackerQuery);
            }

            return result;
        }

        // Increment the 'fldDelete' counter for a specific user and log the action in tblAnalyticsTracker
        public bool AddDelete(int userId)
        {
            // Update the 'tblAnalytics' table
            string updateQuery = $"UPDATE tblAnalytics SET fldDelete = fldDelete + 1 WHERE fldUser_Id = {userId}";
            bool result = adoHelper.CheckInsert(updateQuery) > 0;

            if (result)
            {
                // Get the Analytics ID for the user
                string selectAnalyticsIdQuery = $"SELECT fldAnalytics_Id FROM tblAnalytics WHERE fldUser_Id = {userId}";
                DataTable dt = adoHelper.GetDataTable(selectAnalyticsIdQuery);
                int analyticsId = Convert.ToInt32(dt.Rows[0]["fldAnalytics_Id"]);

                // Insert into 'tblAnalyticsTracker' table
                string insertTrackerQuery = $"INSERT INTO tblAnalyticsTracker (fldAnalytics_Id, fldOperation_Id, fldOperation_Date) " +
                                            $"VALUES ({analyticsId}, 2, CAST(GETDATE() AS DATE))"; // 2 is for 'delete' operation
                adoHelper.CheckInsert(insertTrackerQuery);
            }

            return result;
        }

        public string GetOperationDataForPeriod(int userId, int period, int operationId)
        {
            string file = "ServerDB.mdf"; // Use the appropriate file name for your database
            DateTime currentDate = DateTime.Now;
            DateTime startDate = DateTime.MinValue;

            // Add the date filter condition based on the period
            if (period == 1) // Last month
            {
                startDate = currentDate.AddMonths(-1);
            }
            else if (period == 2) // Last 3 months
            {
                startDate = currentDate.AddMonths(-3);
            }
            else if (period == 3) // Last 6 months
            {
                startDate = currentDate.AddMonths(-6);
            }
            else if (period == 4) // Last year
            {
                startDate = currentDate.AddYears(-1);
            }

            // If the start date is valid, include it in the query
            string dateFilter = (startDate != DateTime.MinValue) ? $"AND fldOperation_Date >= '{startDate:yyyy-MM-dd}'" : "";

            // Query to get the operation count for each date
            string query = $@"
        SELECT fldOperation_Date, COUNT(*) AS OperationCount
        FROM tblAnalyticsTracker
        WHERE fldAnalytics_Id IN (SELECT fldAnalytics_Id FROM tblAnalytics WHERE fldUser_Id = {userId})
          AND fldOperation_Id = {operationId}
          {dateFilter}
        GROUP BY fldOperation_Date
        ORDER BY fldOperation_Date ASC;";

            // Execute the query
            DataTable dt = AdoHelper.GetDataTable(file, query);

            // Prepare the result list
            var result = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime fldDate = Convert.ToDateTime(row["fldOperation_Date"]);
                int operationCount = Convert.ToInt32(row["OperationCount"]);

                // Add the formatted date and count (renamed as value) to the result
                result.Add(new
                {
                    date = fldDate.ToString("yyyy-MM-dd"),  // Format date as 'yyyy-MM-dd'
                    value = operationCount  // Renamed from count to value
                });
            }

            // Return the result as JSON
            return JsonConvert.SerializeObject(result);
        }

    }
}
