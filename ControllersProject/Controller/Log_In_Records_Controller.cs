using ControllersProject.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Controller
{
    public class Log_In_Records_Controller
    {
        private Modal_Log_In_Records mlir = new Modal_Log_In_Records();
        public bool InsertLogInRecord(int userId)
        {
          return mlir.InsertLogRecord(userId);
        }
        
    }
}
