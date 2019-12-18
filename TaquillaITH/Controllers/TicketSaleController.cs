using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using TaquillaITH.Data;
using TaquillaITH.Models;
using TaquillaITH.Models.DTO;
using TaquillaITH.Services;
using TaquillaITH.ViewModels;

namespace TaquillaITH.Controllers
{
    public class TicketSaleController : Controller
    {
        #region Variables and context, etc
        public ApiServices _sc;
        public RestClient _client;
        private readonly ApplicationDbContext _db;
        public TicketSaleController(ApiServices apiServices, ApplicationDbContext db)
        {
            _sc = apiServices;
            _db = db;
            _client = new RestClient();
        }

        public async Task<IActionResult> Step1(string date)
        {
            try
            {
                var FinalDate = new DateTime();

                if (!string.IsNullOrEmpty(date))
                {
                    FinalDate = Convert.ToDateTime(date);
                }
                else
                    FinalDate = DateTime.Now;

                var req = new RestRequest("http://taquilla2.gear.host/api/booth/GetShowTimes")
                {
                    Method = Method.GET,
                    RequestFormat = DataFormat.Json
                };

                //Obtener cartelera de gestion de peliculas
                var resp = await _client.ExecuteGetTaskAsync(req);
                List<Movie> Movies = new List<Movie>();
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Movies = JsonConvert.DeserializeObject<List<Movie>>(resp.Content);
                }

                //var Movies = GetShowTimes(FinalDate.ToString()).ToList();

                if (Movies.Any())
                {
                    ViewData["CurrentDate"] = FinalDate;
                    return View(Movies);
                }

                return NotFound("No se encontraron funciones en esta fecha.");
            }
            catch (Exception ex)
            {
                return NotFound("No se encontraron funciones en esta fecha.");
            }
        }
        #endregion
    }
}
