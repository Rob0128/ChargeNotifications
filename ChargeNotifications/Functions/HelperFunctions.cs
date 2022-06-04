using ChargeNotifications.Data;
using Microsoft.AspNetCore.Components;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System;

namespace ChargeNotifications.Functions
{
    public class HelperFunctions
    {

        public async Task<PdfDocument> CreatePdf() {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //Create PDF Document
            PdfDocument document = new PdfDocument();
            //You will have to add Page in PDF Document
            PdfPage page = document.AddPage();
            //For drawing in PDF Page you will nedd XGraphics Object
            /*XGraphics gfx = XGraphics.FromPdfPage(page);
            //For Test you will have to define font to be used
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);*/
            //Finally use XGraphics & font object to draw text in PDF Page
            /*gfx.DrawString("My First PDF Document", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);
            //Specify file name of the PDF file
            string filename = "FirstPDFDocument.pdf";
                    //Save PDF File
                    document.Save(filename);
            //Load PDF File for viewing
            Process.Start(filename);*/

            return document;

        }

        internal class CreatePdf
        {
            public CreatePdf()
            {
            }
        }
    }
}
