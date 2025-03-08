using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Collections.Specialized;
using MimeKit; // Required for MimeMessage
using MailKit.Net.Smtp; // Required for SmtpClient
using MailKit.Security; // Required for SecureSocketOption
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace WebServiceSoapAPI
{
    /// <summary>
    /// Summary description for EmailSender
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EmailSender : System.Web.Services.WebService
    {

        [WebMethod]
        public string SendEmail(string to, string name, string msg)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Gilad's website", "EmailSender@trial-pq3enl6o1z8l2vwr.mlsender.net"));
            email.To.Add(new MailboxAddress(name, to));

            email.Subject = "Testing out email sending";
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
        public string SendWelcomeEmail(string to, string name,string username,string password)
        {
            // Replace these with actual values
            string apiUrl = "https://api.mailersend.com/v1/email";
            string apiKey = "mlsn.1ad522e2e09577ddbf343e8ea5e1817f66804e8438400fbd8646f005f0a17dc8"; // Your API token

            // Construct the payload
            var payload = new
            {
                from = new { email = "EmailSender@trial-pq3enl6o1z8l2vwr.mlsender.net" },
                to = new[]
                {
            new { email = to }
        },
                variables = new[]
                {
            new
            {
                email = to,
                substitutions = new[]
                {
                    new { var = "name", value = name },
                    new { var = "help_url", value = "https://localhost:44345/View/Send_Review.aspx" },
                    new { var = "username", value = username },
                    new { var = "from.name", value = "Gilad's website" },
                    new { var = "login_url", value = "https://localhost:44345/View/Log_In.aspx" },
                    new { var = "action_url", value = "https://localhost:44345/View/Log_In.aspx" },
                    new { var = "support_url", value = "https://localhost:44345/View/Send_Review.aspx" },
                    new { var = "account.name", value = name },
                    new { var = "password" , value = password}
                }
            }
        },
                template_id = "zr6ke4nrdoylon12"
            };

            // Send the email using HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Add headers
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                    client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                    // Serialize payload to JSON
                    var jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Post the request
                    var response = client.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return "Email sent successfully!";
                    }
                    else
                    {
                        var errorContent = response.Content.ReadAsStringAsync().Result;
                        return $"Failed to send email. Status Code: {response.StatusCode}, Error: {errorContent}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error sending email: {ex.Message}";
                }
            }
        }

        [WebMethod]
        public string SendPlanExpiringEmail(string to, string name, string review, string accountName)
        {
            string apiUrl = "https://api.mailersend.com/v1/email";
            string apiKey = "mlsn.1ad522e2e09577ddbf343e8ea5e1817f66804e8438400fbd8646f005f0a17dc8"; // Your API token

            // Create the payload object with variables
            var payload = new
            {
                from = new { email = "EmailSender@trial-pq3enl6o1z8l2vwr.mlsender.net" },
                to = new[] { new { email = to } },
                subject = "Your Plan is Expiring Soon",
                variables = new[]
                {
            new {
                email = to,
                substitutions = new[]
                {
                    new { var = "name", value = name },
                    new { var = "review", value = review },
                    new { var = "contact_url", value = "Send_Review.aspx" },
                    new { var = "account_name", value = accountName },
                    new {var = "username", value = name}
                }
            }
        },
                template_id = "351ndgw6j9x4zqx8" // Template ID
            };

            // Make the HTTP POST request
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                    client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Synchronous request for simplicity; consider making this async
                    var response = client.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return "Plan expiring email sent successfully!";
                    }
                    else
                    {
                        string errorContent = response.Content.ReadAsStringAsync().Result;
                        return $"Failed to send plan expiring email. Status Code: {response.StatusCode}, Error: {errorContent}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error sending plan expiring email: {ex.Message}";
                }
            }
        }




    }
}