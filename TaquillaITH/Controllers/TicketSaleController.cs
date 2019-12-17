using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public TicketSaleController(ILogger<HomeController> logger, ApiServices apiServices, ApplicationDbContext db)
        {
            _sc = apiServices;
            _db = db;
            _client = new RestClient();
        }

        public IActionResult Step1(EverythingViewModel model, string date)
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

                var Movies = _sc.GetMovies(FinalDate).ToList();

                if (Movies.Any())
                    return View(Movies);
                else
                    return NotFound("No se encontraron funciones en esta fecha.");
            }
            catch (Exception ex)
            {
                return NotFound("No se encontraron funciones en esta fecha.");
            }
        }

        public IActionResult FromStep1To2(EverythingViewModel model)
        {
            return Ok();
        }
        #endregion
    }
}
