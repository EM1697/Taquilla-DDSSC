using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaquillaITH.Models;
using TaquillaITH.Services;
using TaquillaITH.ViewModels;

namespace TaquillaITH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApiServices _sc;
        public HomeController(ILogger<HomeController> logger, ApiServices apiServices)
        {
            _logger = logger;
            _sc = apiServices;
        }

        public IActionResult Index()
        {
            // DateTime myDate = DateTime.ParseExact("2019-12-02 13:09:00.6680000", "yyyy-MM-dd HH:mm:ss.fffffff", null);
            // var sales = _sc.GetDaySales(myDate);
            // ViewData["DaySales"] = sales;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public List<DaySalesViewModel> DaySales(string date){
            // var DaySales = _sc.GetDaySales(date).Where(x => x.SaleDate.Day == date.Day);
            DateTime myDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", null);
            var daySales = _sc.GetDaySales(date);
            return (daySales);
        }

        [HttpPost]
        public async Task<ActionResult<DaySales>> PostTodoItem(List<DaySalesViewModel> sales)
        {
            DaySales daySales = new DaySales();
            int normalTickets = 0;
            decimal normalTicketsAmount = 0;
            int Tickets3D = 0;
            decimal Tickets3DAmount = 0;
            int VipTicket = 0;
            decimal VipTicketAmount = 0;
            decimal TotalAmount = 0;
            DateTime saleDate = sales.FirstOrDefault().SaleDate;
            foreach (var sale in sales)
            {
                switch (sale.TipoBoletoId)
                {
                    case CineTaquilla.Helpers.TicketType.NormalTicket:
                        normalTickets += 1; break;
                    case CineTaquilla.Helpers.TicketType.Ticket3D:
                        Tickets3D += 1; break;
                    case CineTaquilla.Helpers.TicketType.VipTicket:
                        VipTicket += 1; break;
                }
            }
            normalTicketsAmount = normalTickets * 50;
            Tickets3DAmount = Tickets3D * 60;
            VipTicketAmount = VipTicket * 70;
            daySales.NomalTicketsAmount = normalTicketsAmount;
            daySales.Tickets3DAmount = Tickets3DAmount;
            daySales.


            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();


            //var req = new RestRequest("http://cinefinanzas.gear.host/api/Finance/IncomeRegister%22)
            //        {
            //        Method = Method.POST,
            //        RequestFormat = DataFormat.Json
            //    };

            //req.AddHeader("Content-Type", "multipart/form-data");
            //var resp = await _client.ExecutePostTaskAsync(req);
            //if (resp.StatusCode == System.Net.HttpStatusCode.OK)

                //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

    }
}
