using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Collections.Specialized;
using MimeKit; // Required for MimeMessage
using MailKit.Net.Smtp; // Required for SmtpClient
using MailKit.Security; // Required for SecureSocketOptions

namespace EmailSoapAPI
{
    /// <summary>
    /// Summary description for Practice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Practice : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string SendEmail(string to,string name, string msg)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("OneLife", "EmailSendertest@trial-pq3enl6o1z8l2vwr.mlsender.net"));
            email.To.Add(new MailboxAddress(name, to));

            email.Subject = "את אמא תחת";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = msg
            };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    // Connect to SMTP server with TLS
                    smtp.Connect("smtp.mailersend.net", 587, SecureSocketOptions.StartTls);

                    // Authenticate with your email and password (or App Password for Gmail with 2FA)
                    smtp.Authenticate("MS_lxauPl@trial-pq3enl6o1z8l2vwr.mlsender.net", "yIuM4Yw1OKQroNFc"); // Use App Password if 2FA enabled

                    // Send the email
                    smtp.Send(email);

                    // Disconnect after sending the email
                    smtp.Disconnect(true);

                    return "success";  // Return true if the email was sent successfully
                }
                catch (Exception ex)
                {
                    // Log the exception or handle the error
                    return ("Error sending email: " + ex.Message);
                }

            }
        }
       

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
