﻿using ChargeNotifications.Data;
using Microsoft.AspNetCore.Components;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System;
using OpenHtmlToPdf;
using System.Text;

namespace ChargeNotifications.Functions
{
    public class HelperFunctions
    {

        public async static Task CreatePdf(int Id, DateTime recordDate, int cost, String desc) {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            

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