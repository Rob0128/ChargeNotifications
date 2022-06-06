using ChargeNotifications.Data;
using ChargeNotifications.Functions;
using Microsoft.AspNetCore.Mvc;
using ChargeNotifications.Models;
using Microsoft.Data.SqlClient;

namespace ChargeNotifications.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ChargeController : ControllerBase
    {

        private readonly ChargeContext _context;
        private object reader;

        public ChargeController(ChargeContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GenPDF()
        {

            List<Charge> charges = null;

            var dateUsed = DateTime.Today.AddDays(-3);
            string sqlFormattedDate = dateUsed.ToString("yyyy-MM-dd HH:mm:ss.fff");

            await using (SqlConnection cn = new SqlConnection("Server = localhost; Database = master; Trusted_Connection = True;"))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Charge", cn);
                var datareader = cmd.ExecuteReader();
                charges = GetList<Charge>(datareader);
            }

            SeparateListAndGenPdfs(charges);
                                
            return charges == null ? NotFound() : Ok(charges);

        }

        private List<T>? GetList<T>(SqlDataReader datareader)
        {
            List<T>? list = new List<T>();
            while (datareader.Read())
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach (var item in type.GetProperties())
                {
                    var propType = item.PropertyType;
                    item.SetValue(obj, Convert.ChangeType(datareader[item.Name].ToString(), propType));
                }
                list.Add(obj);
            }
            return list;
        }

        private async void SeparateListAndGenPdfs(List<Charge> data)
        {
            List<Charge>? Game1 = new List<Charge>();
            List<Charge>? Game2 = new List<Charge>();
            List<Charge>? Game3 = new List<Charge>();

            var prevId = 0;
            foreach (var item in data)
            {

                if (prevId == item.CustomerId && prevId > 0)
                {
                    if (item.Description == "Game 1")
                    {
                        Game1.Add(item);
                    }
                    else if (item.Description == "Game 2")
                    {
                        Game2.Add(item);
                    }
                    else if (item.Description == "Game 3")
                    {
                        Game3.Add(item);
                    }
                }
                else if (prevId > 0)
                {

                    //GenerateChargePdf pdf
                    await HelperFunctions.CreatePdf(Game1, Game2, Game3);

                    //reset lists to empty for the next iteration
                    prevId = item.CustomerId;
                    Game1 = new List<Charge>();
                    Game2 = new List<Charge>();
                    Game3 = new List<Charge>();
                }
                else
                {
                    prevId = item.CustomerId;
                }

            }


        }

       

    }

}
