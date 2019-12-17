using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public IActionResult Step1(EverythingViewModel model)
        {
            var Movies = _sc.GetMovies().ToList();
            return View(Movies);
        }

        public IActionResult FromStep1To2(EverythingViewModel model)
        {
            return Ok();
        }
        #endregion
    }
}
