using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for MyAdoHelper
/// פעולות עזר לשימוש במסד נתונים  מסוג 
/// SQL SERVER
///  App_Data המסד ממוקם בתקיה 
/// </summary>
    public class AdoHelper{
        public AdoHelper(){
            //
            // TODO: Add constructor logic here
            //
        }

        public static SqlConnection ConnectToDb(string fileName){ 
            string path = HttpContext.Current.Server.MapPath("App_Data/" + fileName);//מאתר את מיקום מסד הנתונים מהשורש ועד התקייה בה ממוקם המסד
            string connString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + path + "; Integrated Security = True";

            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }

        //הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה אמת אם הנתונים קיימים ושקר אחרת
        public static bool IsExist(string fileName, string sql) {
            SqlConnection conn = ConnectToDb(fileName);
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            SqlDataReader data = com.ExecuteReader();
            bool found;
            found = (bool)data.Read();// אם יש נתונים לקריאה יושם אמת אחרת שקר - הערך קיים במסד הנתונים
            conn.Close();
            return found;
        }

        public static DataTable GetDataTable(string fileName, string sql){
            SqlConnection conn = ConnectToDb(fileName);
            conn.Open();
            SqlDataAdapter tableAdapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            tableAdapter.Fill(dt);
            return dt;
        }
    }

