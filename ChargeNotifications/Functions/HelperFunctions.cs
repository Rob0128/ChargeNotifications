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
            var game1Charge = 0;
            var game2Charge = 0;
            var game3Charge = 0;

            if (game1.Count != 0) { 
                
                Id = game1.First().CustomerId;
                recordDate = game1.First().ChargeDate;
                game1Charge = game1.First().CostPence;
            }
            else if (game2.Count != 0) { 
                
                Id = game2.First().CustomerId;
                recordDate = game2.First().ChargeDate;
                game2Charge = game2.First().CostPence;

            }
            else if (game3.Count != 0)
            {
                
                Id = game3.First().CustomerId;
                recordDate = game3.First().ChargeDate;
                game3Charge = game3.First().CostPence;

            }

            //will need to create an "Invoices" folder to set up
            String filename = "Invoices/Recipt_Id-" + Id.ToString() + "_" + recordDate.ToFileTime().ToString().Replace("/", "_");

            String htmlFilename = filename + ".html";

            String pdfFilename = filename + ".pdf";

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    await w.WriteLineAsync("<div style=\"height:100px\"></div>");
                    await w.WriteLineAsync("<div style=\"margin-left:100px\">Customer Number: " + Id.ToString() + "</div>");
                    await w.WriteLineAsync("<br>");
                    await w.WriteLineAsync("<div style=\"margin-left:100px\">Charge Date: " + recordDate.ToString() + "</div>" + "<br>" );
                    await w.WriteLineAsync("<div style=\"height:100px\"></div>");
                    await w.WriteLineAsync("<div style=\"margin-left:200px\">Game 1</div>");
                    foreach (var purchase in game1)
                    {
                        await w.WriteLineAsync("<div style=\"height:100px; margin-left:200px\">" + purchase.ChargeDescription + " | " + "Cost Pence: " + purchase.CostPence.ToString()
                                                                        + " | " + "Cost Total: " + purchase.CostTotal.ToString() + "</div>");
                    }
                    await w.WriteLineAsync("<div style=\"margin-left:200px\">Game 2</div>");
                    foreach (var purchase in game2)
                    {
                        await w.WriteLineAsync("<div style=\"height:100px; margin-left:200px\">" + purchase.ChargeDescription + " | " + "Cost Pence: " + purchase.CostPence.ToString()
                                                                        + " | " + "Cost Total: " + purchase.CostTotal.ToString() + "</div>");
                    }
                    await w.WriteLineAsync("<div style=\"margin-left:200px\">Game 3</div>");
                    foreach (var purchase in game3)
                    {
                        await w.WriteLineAsync("<div style=\"height:100px; margin-left:200px\">" + purchase.ChargeDescription + " | " + "Cost Pence: " + purchase.CostPence.ToString()
                                                                        + " | " + "Cost Total: " + purchase.CostTotal.ToString() + "</div>");
                    }
                }
            }


            var html = File.ReadAllText(filename);

            var pdf = OpenHtmlToPdf.Pdf.From(html).OfSize(PaperSize.A4).WithMargins(0.Centimeters()).Landscape().Content();

            File.WriteAllBytes(pdfFilename, pdf);
                       
        }

       
    }
}
