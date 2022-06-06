using ChargeNotifications.Data;
using Microsoft.AspNetCore.Components;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System;
using OpenHtmlToPdf;
using System.Text;
using ChargeNotifications.Models;

namespace ChargeNotifications.Functions
{
    public class HelperFunctions
    {

        public async static Task CreatePdf(List<Charge> game1, List<Charge> game2, List<Charge> game3) {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            var Id = 0;
            var recordDate = new DateTime(2015, 12, 25);
            if (game1 != null) { 
                
                Id = game1.First().CustomerId;
                recordDate = game1.First().ChargeDate;
            }
            else if (game2 != null) { 
                
                Id = game2.First().CustomerId;
                recordDate = game2.First().ChargeDate;
            }
            else if (game3 != null)
            {
                
                Id = game3.First().CustomerId;
                recordDate = game3.First().ChargeDate;
            }

            String filename = "Invoices/Recipt_Id-" + Id.ToString() + "_" + recordDate.ToFileTime().ToString().Replace("/", "_");

            String htmlFilename = filename + ".html";

            String pdfFilename = filename + ".pdf";

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    await w.WriteLineAsync("<div style=\"height:100px\"></div>");
                    await w.WriteLineAsync("Customer Number: " + Id.ToString());
                    await w.WriteLineAsync("<br>");
                    await w.WriteLineAsync("Customer Name: " + Id.ToString() +"<br>" );
                   
                }
            }


            var html = File.ReadAllText(filename);

            var pdf = OpenHtmlToPdf.Pdf.From(html).OfSize(PaperSize.A4).WithMargins(0.Centimeters()).Landscape().Content();

            File.WriteAllBytes(pdfFilename, pdf);
                       
        }

       
    }
}
