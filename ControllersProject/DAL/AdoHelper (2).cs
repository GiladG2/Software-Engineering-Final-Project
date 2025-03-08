using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ControllersProject.DAL
{
    public class AdoHelper
    {
        private string dbPath;

        public AdoHelper()
        {
            bool res = false;
            dbPath = res ? "ServerDB.mdf" : "DB.mdf";
        }

        public AdoHelper(string dbName)
        {
            dbPath = dbName;
        }

        public static SqlConnection ConnectToDb(string fileName)
        {
            fileName = FindDatabase(fileName);

            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                throw new FileNotFoundException($"Database file not found at: {fileName}");
            }

            string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated Security=True";
            return new SqlConnection(connString);
        }

        public SqlConnection ConnectToDb()
        {
            dbPath = FindDatabase(dbPath);
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath))
            {
                throw new FileNotFoundException($"Database file not found at: {dbPath}");
            }

            string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True";
            return new SqlConnection(connString);
        }

        // Check if a record exists (default database)
        public bool IsExist(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                bool found = reader.Read();
                conn.Close();
                return found;
            }
        }

        // Check if a record exists (specific database file)
        public static bool IsExist(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                bool found = reader.Read();
                conn.Close();
                return found;
            }
        }

        // Async version for checking if a record exists (default database)
        public async Task<bool> IsExistAsync(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                bool found = await reader.ReadAsync();
                return found;
            }
        }

        // Async version for checking if a record exists (specific database file)
        public static async Task<bool> IsExistAsync(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                bool found = await reader.ReadAsync();
                return found;
            }
        }

        // Get DataTable (default database)
        public DataTable GetDataTable(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                conn.Open();
                SqlDataAdapter tableAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                tableAdapter.Fill(dt);
                return dt;
            }
        }

        // Get DataTable (specific database file)
        public static DataTable GetDataTable(string fileName, string query)
        {
            fileName = FindDatabase(fileName);
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                conn.Open();
                SqlDataAdapter tableAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                tableAdapter.Fill(dt);
                return dt;
            }
        }

        // Async version of GetDataTable (default database)
        public async Task<DataTable> GetDataTableAsync(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                await conn.OpenAsync();
                SqlDataAdapter tableAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                await Task.Run(() => tableAdapter.Fill(dt)); // Fill is synchronous, so we use Task.Run
                return dt;
            }
        }

        // Async version of GetDataTable (specific database file)
        public static async Task<DataTable> GetDataTableAsync(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                await conn.OpenAsync();
                SqlDataAdapter tableAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                await Task.Run(() => tableAdapter.Fill(dt)); // Fill is synchronous, so we use Task.Run
                return dt;
            }
        }

        // Get DataSet (default database)
        public DataSet GetDataSet(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }

        // Get DataSet (specific database file)
        public static DataSet GetDataSet(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }

        // Async version of GetDataSet (default database)
        public async Task<DataSet> GetDataSetAsync(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                await conn.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                await Task.Run(() => adapter.Fill(ds)); // Fill is synchronous, so we use Task.Run
                return ds;
            }
        }

        // Async version of GetDataSet (specific database file)
        public static async Task<DataSet> GetDataSetAsync(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                await conn.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                await Task.Run(() => adapter.Fill(ds)); // Fill is synchronous, so we use Task.Run
                return ds;
            }
        }

        // Execute Insert/Update/Delete (default database)
        public int CheckInsert(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        // Execute Insert/Update/Delete (specific database file)
        public static int CheckInsert(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        // Async version of CheckInsert (default database)
        public async Task<int> CheckInsertAsync(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected;
            }
        }

        // Async version of CheckInsert (specific database file)
        public static async Task<int> CheckInsertAsync(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected;
            }
        }

        // Execute Scalar (default database)
        public object ExecuteScalar(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                return result;
            }
        }

        // Execute Scalar (specific database file)
        public static object ExecuteScalar(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                return result;
            }
        }

        // Async version of ExecuteScalar (default database)
        public async Task<object> ExecuteScalarAsync(string query)
        {
            using (SqlConnection conn = ConnectToDb())
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = await cmd.ExecuteScalarAsync();
                return result;
            }
        }

        // Async version of ExecuteScalar (specific database file)
        public static async Task<object> ExecuteScalarAsync(string fileName, string query)
        {
            using (SqlConnection conn = AdoHelper.ConnectToDb(fileName))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = await cmd.ExecuteScalarAsync();
                return result;
            }
        }


        public static string FindDatabase(string dbName)
        {
            // Define possible directory paths within the "Project_Jim" folder
            var possibleBaseDirectories = new[]
            {
        "Project_Jim\\PlanDataWebService\\App_Data",
        "Project_Jim\\Project_Gym\\App_Data"
    };

            // Get all drives currently available on the system
            DriveInfo[] drives = DriveInfo.GetDrives();

            // Loop through each available drive
            foreach (DriveInfo drive in drives)
            {
                // Only check the drive if it's ready (e.g., USB drives or external drives can sometimes not be ready)
                if (drive.IsReady)
                {
                    // Loop through each possible directory structure
                    foreach (string baseDirectory in possibleBaseDirectories)
                    {
                        // Construct the full path starting from the root of the current drive
                        string fullPath = Path.Combine(drive.Name, "c#", "אינטרנט", "פרויקט יא יב", baseDirectory);

                        // Check if the directory exists on the current drive
                        if (Directory.Exists(fullPath))
                        {
                            // Check for the database file within the directory
                            string possibleDbPath = Path.Combine(fullPath, dbName);
                            if (File.Exists(possibleDbPath))
                            {
                                return possibleDbPath; // Return the full path if the file exists
                            }
                        }
                    }
                }
            }

            // Return null if the database was not found
            return null;
        }

    }
}
