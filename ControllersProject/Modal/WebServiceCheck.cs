using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ControllersProject.Modal
{
    internal class WebServiceCheck
    {
        private static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Checks if the SOAP web service is up by sending a GET request.
        /// </summary>
        /// <param name="serviceUrl">The URL of the SOAP web service.</param>
        /// <returns>True if the service is up; false otherwise.</returns>
        public bool IsServiceUp(string serviceUrl)
        {
            try
            {
                // Send a synchronous GET request to the service URL
                HttpResponseMessage response = httpClient.GetAsync(serviceUrl).GetAwaiter().GetResult();

                // If status code is 200, the service is up
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                Console.WriteLine($"Error checking web service: {ex.Message}");
                return false;
            }
        }
    }
}
