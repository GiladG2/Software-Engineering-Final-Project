using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using SixLabors.ImageSharp.PixelFormats;
using Aspose.Pdf.Operators;
using Microsoft.AspNetCore.Mvc;
using System.Web.Caching;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Aspose.Foundation.UriResolver.RequestResponses;
using ControllersProject.Controller;

namespace Project_Gym.Controller
{
    public class Xml_Controller
    {
        static int id = 0;
        int reviewsall = 0;
        string reviews = "";
        Users_Controller cu = new Users_Controller();
        public string GetResponses(string username)
        {
            string FilePath = HttpContext.Current.Server.MapPath("~/App_Data/XML/responses.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(FilePath);
            XmlNodeList responses = xmldoc.GetElementsByTagName("response");
            string reviews = string.Empty; // Initialize the reviews variable

            foreach (XmlNode response in responses)
            {
                // Retrieve child elements and attributes
                XmlNode sendTo = response["username"];
                XmlNode writer = response["writer"];
                XmlNode title = response["title"];
                XmlNode content = response["content"];
                XmlNode date = response["date"];
                string status = response.Attributes?["status"]?.Value ?? "No Status"; // Get status attribute from response
                string id = response.Attributes?["id"]?.Value;
                if (sendTo != null && sendTo.InnerText.Equals(username))
                {
                    reviews += "<div class='review'>";
                    reviews += cu.GetAccessKey(writer?.InnerText) >= 7
                        ? $"<h3 id='username'>Written by: [Admin] {writer?.InnerText}</h3>"
                        : $"<h3 id='username'>Written by: {writer?.InnerText}</h3>";
                    reviews += $"<h4 id='{id}status'>{status}</h4>";
                    reviews += $"<h3>Title: <strong class='title'>{title?.InnerText}</strong></h3>";
                    reviews += $"<button class='button1' onclick='seer(\"{content?.InnerText}\",\"{writer?.InnerText}\",\"{title?.InnerText}\",\"{id}\",\"~/App_Data/XML/responses.xml\")'>See full review</button>";
                    reviews += $"<h3>The response was written at: {date?.InnerText}</h3>";
                    reviews += "</div>";
                }
            }

            return reviews;
        }

        public string UpdateStatus(int id, string filePath)
        {
            try
            {
                string resolvedPath = HttpContext.Current.Server.MapPath(filePath);

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(resolvedPath);
                XmlNode response = null;
                if (filePath.Contains("responses"))
                    response = xmldoc.SelectSingleNode($"//response[@id='{id}']");
                else
                    response = xmldoc.SelectSingleNode($"//review[@id='{id}']");
                if (response != null)
                {
                    string currentStatus = response.Attributes["status"]?.Value;

                    if (currentStatus == "Not seen")
                    {
                        response.Attributes["status"].Value = "Seen";
                        xmldoc.Save(resolvedPath);
                        return "Status updated successfully.";
                    }
                }

                return "No changes were made.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public string DisplayAdminReviews()
        {
            string FilePath = HttpContext.Current.Server.MapPath("~/App_Data/XML/reviews.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(FilePath);
            XmlNodeList reviews = xmldoc.GetElementsByTagName("review"); // Use review instead of response
            string reviewsHtml = string.Empty; // Initialize reviews string

            foreach (XmlNode review in reviews)
            {
                // Retrieve child elements and attributes from the review node
                XmlNode writer = review["username"];
                XmlNode title = review["title"];
                XmlNode comment = review["comment"];
                XmlNode rating = review["rating"];
                XmlNode date = review["date"];
                string status = review.Attributes?["status"]?.Value ?? "No Status"; // Get status attribute from review
                string id = review.Attributes?["id"]?.Value;

                // Generate the HTML for each review, following the same format as GetResponses
                reviewsHtml += "<div class='review'>";

                // Display rating and status
                reviewsHtml += $"<span class='star'>{rating?.InnerText}</span>";
                reviewsHtml += $"<h4 id='{id}status'>{status}</h4>";

                // Display writer's information with check for Admin access
                reviewsHtml += $"<h3 id='username'>Written by: {writer?.InnerText}</h3>";

                // Display the review title and button to see the full comment
                reviewsHtml += $"<h3>Title: <strong class='title'>{title?.InnerText}</strong></h3>";
                reviewsHtml += $"<button class='button1' onclick='seer(\"{comment?.InnerText}\",\"{writer?.InnerText}\",\"{title?.InnerText}\",\"{id}\",\"~/App_Data/XML/reviews.xml\")'>See full review</button>";

                // Display the date the review was written
                reviewsHtml += $"<h3>The review was written at: {date?.InnerText}</h3>";

                reviewsHtml += "</div>";
            }

            return reviewsHtml;
        }
        public int GetMaxId(string filePath)
        {
            string idFilePath = HttpContext.Current.Server.MapPath("~/App_Data/XML/id.xml");

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(idFilePath);

            // Look for the Storage element with the matching filePath
            XmlNode storageNode = xmldoc.SelectSingleNode($"/All/Storage[filePath='{filePath}']");

            if (storageNode != null)
            {
                // Get the Id for this filePath
                XmlNode idNode = storageNode["Id"];
                if (idNode != null && int.TryParse(idNode.InnerText, out int currentId))
                {
                    return currentId;
                }
            }

            // If no entry exists, return 0 or default value (indicating no IDs have been used yet)
            return 0;
        }


        public void UpdateId(string filePath)
        {
            string idFilePath = HttpContext.Current.Server.MapPath("~/App_Data/XML/id.xml");

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(idFilePath);

            // Find the storage node that corresponds to this filePath
            XmlNode storageNode = xmldoc.SelectSingleNode($"/All/Storage[filePath='{filePath}']");

            if (storageNode != null)
            {
                // Get the ID node and update its value
                XmlNode idNode = storageNode["Id"];
                if (idNode != null)
                {
                    int currentId = int.Parse(idNode.InnerText);
                    idNode.InnerText = (currentId + 1).ToString();
                }
            }
            else
            {
                // If the filePath is not found, create a new Storage element
                XmlElement storageElement = xmldoc.CreateElement("Storage");

                XmlElement filePathElement = xmldoc.CreateElement("filePath");
                filePathElement.InnerText = filePath;

                XmlElement idElement = xmldoc.CreateElement("Id");
                idElement.InnerText = "1"; // Start with ID 1 if no previous ID exists.

                storageElement.AppendChild(filePathElement);
                storageElement.AppendChild(idElement);

                xmldoc.DocumentElement.AppendChild(storageElement);
            }

            // Save the updated XML
            xmldoc.Save(idFilePath);
        }

        public void InsertToXml(string response, string userIdTo, string title, string writer)
        {
            XmlDocument xmldoc = new XmlDocument();
            string FilePath = HttpContext.Current.Server.MapPath("~/App_Data/XML/responses.xml");

            // Load the existing XML file
            xmldoc.Load(FilePath);

            // Create new elements for the response
            XmlElement responseElement = xmldoc.CreateElement("response");
            XmlElement contentElement = xmldoc.CreateElement("content");
            XmlElement userToSendElement = xmldoc.CreateElement("username");
            XmlElement dateElement = xmldoc.CreateElement("date");
            XmlElement writerElement = xmldoc.CreateElement("writer");
            XmlElement titleElement = xmldoc.CreateElement("title");

            // Populate the content of the elements
            contentElement.InnerText = response;
            userToSendElement.InnerText = userIdTo;
            writerElement.InnerText = writer;
            titleElement.InnerText = title;

            // Set the current date in dd/MM/yyyy format
            DateTime currentDate = DateTime.Now;
            dateElement.InnerText = currentDate.ToString("dd/MM/yyyy");
            // Set the "status" attribute for the response element

            int currentId = GetMaxId(FilePath) + 1;

            responseElement.SetAttribute("status", "Not seen");
            responseElement.SetAttribute("id", $"{currentId}");
            // Append child elements to the response element
            responseElement.AppendChild(userToSendElement);
            responseElement.AppendChild(writerElement);
            responseElement.AppendChild(contentElement);
            responseElement.AppendChild(dateElement);
            responseElement.AppendChild(titleElement);

            // Insert the response element at the top of the document
            xmldoc.DocumentElement.InsertBefore(responseElement, xmldoc.DocumentElement.FirstChild);

            // Save the updated XML to the file
            using (FileStream xmlf = new FileStream(FilePath, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
            {
                xmldoc.Save(xmlf);
            }
            UpdateId(FilePath);

        }

        public string InsertAdminReviews(string username, string date, string rating, string comment, string title)
        {

            string stars = "";
            for (int i = 0; i < int.Parse(rating); i++)
                stars += "<i class=\"fa-solid fa-star\" style=\"color: #f6e310;\"></i>";
            XmlDocument xmldoc = new XmlDocument();
            string FilePath = HttpContext.Current.Server.MapPath("~/App_Data/XML/reviews.xml");
            int currentId = GetMaxId(FilePath) + 1;
            xmldoc.Load(FilePath);
            XmlElement reviewelemnt, contentelement, ratingelement, usernameelement, dateelement, titleelement;
            titleelement = xmldoc.CreateElement("title");
            contentelement = xmldoc.CreateElement("comment");
            usernameelement = xmldoc.CreateElement("username");
            ratingelement = xmldoc.CreateElement("rating");
            dateelement = xmldoc.CreateElement("date");
            reviewelemnt = xmldoc.CreateElement("review");

            reviewelemnt.SetAttribute("status", "Not seen");
            reviewelemnt.SetAttribute("id", $"{currentId}");

            contentelement.InnerText = comment;
            ratingelement.InnerText = stars;
            usernameelement.InnerText = username;
            dateelement.InnerText = date;
            titleelement.InnerText = title;
            reviewelemnt.AppendChild(titleelement);
            reviewelemnt.AppendChild(usernameelement);
            reviewelemnt.AppendChild(contentelement);
            reviewelemnt.AppendChild(ratingelement);
            reviewelemnt.AppendChild(dateelement);
            xmldoc.DocumentElement.InsertBefore(reviewelemnt, xmldoc.DocumentElement.FirstChild);
            FileStream xmlf = new FileStream(FilePath, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            xmldoc.Save(xmlf);
            xmlf.Close();
            UpdateId(FilePath);
            return "Your review has been successfuly sent";
        }
        public string GetUnseenResponses(string username)
        {
            string FilePath = HttpContext.Current.Server.MapPath("~/App_Data/XML/responses.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(FilePath);
            XmlNodeList responses = xmldoc.GetElementsByTagName("response");
            int counter = 0;
            foreach (XmlNode response in responses)
            {
                XmlNode sendTo = response["username"];
                if (sendTo != null && sendTo.InnerText.Equals(username))
                {
                    string status = response.Attributes?["status"]?.Value ?? "No Status";
                    if(status.Equals("Not seen"))
                            counter++;
                }

            }
            if (counter == 0)
                return "";
            return counter.ToString();
        }
    }

}

