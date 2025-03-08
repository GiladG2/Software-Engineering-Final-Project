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

namespace EmailSenderSoapAPI
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

            email.From.Add(new MailboxAddress("Gilad's website", "EmailSendertest@trial-pq3enl6o1z8l2vwr.mlsender.net"));
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
    }
}
