using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

namespace WebServiceSoapAPI
{
    /// <summary>
    /// Summary description for SMSSender
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SMSSender : System.Web.Services.WebService
    {

       
        [WebMethod]
        public string sendSMS()
        {
            try
            {
                String message = HttpUtility.UrlEncode("This is your message");
                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
            {
                {"apikey", "NjEzMTY3NDE3ODQxNTQ2YTRjNzQ1MzM0NzU0ZDMzNDU="},
                {"numbers", "972586872259"},
                {"message", message},
                {"sender", "Jims"},
                {"test","true"}
            });

                    string result = System.Text.Encoding.UTF8.GetString(response);

                    // Log the response for debugging
                    System.Diagnostics.Debug.WriteLine("API Response: " + result);

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Log and return the exception message for troubleshooting
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                return $"Error: {ex.Message}";
            }
        }
    }
}
