﻿using ChargeNotifications.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ChargeNotifications.Functions;
using Microsoft.AspNetCore.OData.Query;
using System.Data.OleDb;
using ChargeNotifications.Models;
using Microsoft.EntityFrameworkCore;
using ChargeNotifications.Functions;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Linq;

namespace ChargeNotifications.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ChargeController : ControllerBase
    {

        private readonly ChargeContext _context;
        public ChargeController(ChargeContext context) => _context = context;


        /*
                [HttpGet]
                public async Task<IEnumerable<Charge>> Get() =>
                     await _context.Charge.Where(c => c.ChargeDate == DateO);*/


        [HttpGet("id")]
        public async Task<IActionResult> GetById(int Id)
        {

            /*Validate this payload*/
            /*Accept a customer ID then send request to the db and return a Charge in PDF form for that customers charges*/

            var dateUsed = DateTime.Today.AddDays(-1);

            var charges = await _context.Charge.Where(c => c.ChargeDate == dateUsed).GroupBy(c => new { c.CustomerId, c.CostPence, c.Description }).Select(c => new Charge { Id = c.Key.CustomerId, CostPence = c.Key.CostPence, Description = c.Key.Description }).ToListAsync();

            var test = from c in _context.Charge
                        where c.ChargeDate == dateUsed
                        select c;

            
            /*var grpCharges = await charges.GroupBy(c => new { c.CustomerId }).ToListAsync();*/

            Parallel.ForEach(charges, async charge => { 
                /*foreach(var charge in group)
                {*/
                    await HelperFunctions.CreatePdf(charge.Id, dateUsed, charge.CostPence, charge.Description);
              /*  };*/

            });

          
            return charges == null ? NotFound() : Ok(charges);



            /*var count = 0;
            List<String> resp = new List<String>();
            foreach (var item in payload)
            {
                count++;
                if (count > 2)
                {
                    resp.Add(item.ToString());

                }
                else
                {

                }
            }*/
            /*HttpClient ht = new HttpClient();
            //var thisId = JObject.Parse(payload.ToString());
            var x = await ht.GetAsync("https://localhost:7110/WeatherForecast?$select=Summary").ConfigureAwait(false);

            return Ok(x);
    */
            /* }*/

            /*   [HttpGet]
               public List<Charge> Get()
               {
                   var x = _context.Charge.Where(c => c.ChargeDate == DateTime.Today.AddDays(-1)).ToList();
                   return x;
               }*/

        }

        }

}
