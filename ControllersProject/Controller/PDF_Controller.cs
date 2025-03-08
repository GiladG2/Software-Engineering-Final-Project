
using System;
using System.Collections.Generic;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace ControllersProject.Controller
{ 
public class PDF_Controller
{
        public string CreatePdfFile(string username, LinkedList<string> pdfResponse)
        {
            // Set the license type to Community
            QuestPDF.Settings.License = LicenseType.Community;

            string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads", "PDF");
            Directory.CreateDirectory(text);
            string text2 = Path.Combine(text, username + "_Plan.pdf");

            // Generate PDF document
            GenerateExtensions.GeneratePdf((IDocument)(object)Document.Create((Action<IDocumentContainer>)delegate (IDocumentContainer container)
            {
                PageExtensions.Page(container, (Action<PageDescriptor>)delegate (PageDescriptor page)
                {
                    page.Margin(50f, (Unit)0);
                    ColumnExtensions.Column(page.Content(), (Action<ColumnDescriptor>)delegate (ColumnDescriptor column)
                    {
                        TextSpanDescriptorExtensions.Bold<TextBlockDescriptor>(TextSpanDescriptorExtensions.FontSize<TextBlockDescriptor>(TextExtensions.Text(column.Item(), "Workout Plan"), 20f)).AlignCenter();
                        TextSpanDescriptorExtensions.FontSize<TextBlockDescriptor>(TextExtensions.Text(column.Item(), "Username: " + username), 12f).AlignCenter();
                        PaddingExtensions.PaddingVertical(column.Item(), 10f, (Unit)0);
                        foreach (string item in pdfResponse)
                        {
                            TextSpanDescriptorExtensions.FontSize<TextBlockDescriptor>(TextExtensions.Text(column.Item(), item), 12f).AlignLeft();
                        }
                    });
                });
            }), text2);

            return "PDF saved successfully at: " + text2;
        }

        public string CreatePdf(string username)
    {
        string text = "<html><body><h1>Hello, " + username + "</h1><p>This is your custom plan rendered as HTML in the PDF.</p></body></html>";
        string text2 = "D:\\c#\\אינטרנט\\פרויקט יא יב\\Project_Jim\\Project_Gym\\Downloads\\PDF";
        Directory.CreateDirectory(text2);
        string text3 = Path.Combine(text2, username + "_Plan.pdf");
        GenerateExtensions.GeneratePdf((IDocument)(object)Document.Create((Action<IDocumentContainer>)delegate (IDocumentContainer container)
        {
            PageExtensions.Page(container, (Action<PageDescriptor>)delegate (PageDescriptor page)
            {
                page.Margin(50f, (Unit)0);
                ColumnExtensions.Column(page.Content(), (Action<ColumnDescriptor>)delegate (ColumnDescriptor column)
                {
                    TextSpanDescriptorExtensions.Bold<TextBlockDescriptor>(TextSpanDescriptorExtensions.FontSize<TextBlockDescriptor>(TextExtensions.Text(column.Item(), "Hello, " + username), 20f)).AlignCenter();
                    TextSpanDescriptorExtensions.FontSize<TextBlockDescriptor>(TextExtensions.Text(column.Item(), "This is your custom plan rendered as HTML in the PDF."), 12f).AlignCenter();
                });
            });
        }), text3);
        return "PDF saved successfully at: " + text3;
    }
}

}
