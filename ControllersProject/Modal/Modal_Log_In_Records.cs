using ControllersProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    public class Modal_Log_In_Records
    {
        private readonly string file = "DB.mdf";

        public bool InsertLogRecord(int userId)
        {
            string query = $"INSERT INTO tblLogRecords (fldUser_Id, fldLog_In_Date) VALUES ({userId}, GETDATE())";
            return AdoHelper.CheckInsert(file, query) > 0;
        }

    }
}
