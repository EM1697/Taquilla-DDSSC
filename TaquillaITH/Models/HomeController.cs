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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApiServices _sc;
        public RestClient _client;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApiServices apiServices, ApplicationDbContext db)
        {
            _logger = logger;
            _sc = apiServices;
            _db = db;
            _client = new RestClient();
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
        public async Task<IActionResult> PostTodoItem(string date)
        {
            DateTime datep = Convert.ToDateTime(date);
            var sales =_db.Sales.Where(x => x.SaleDate.Day == datep.Day).ToList();
            if (!sales.Any())
            {
                return BadRequest("No hay");
            }
            DaySales daySales = new DaySales();
            int normalTickets = 0;
            decimal normalTicketsAmount = 0;
            int Tickets3D = 0;
            decimal Tickets3DAmount = 0;
            int VipTicket = 0;
            decimal VipTicketAmount = 0;
            decimal TotalAmount = 0;
            DateTime saleDate = sales.FirstOrDefault().SaleDate;
            List<Producto> productos = new List<Producto>();
            string nombre;
            int precio;
            foreach (var sale in sales)
            {
                switch (sale.TipoBoletoId)
                {
                    case CineTaquilla.Helpers.TicketType.NormalTicket:
                        normalTickets += 1;
                        nombre = "Boleto Normal";
                        precio = 50;
                        break;
                    case CineTaquilla.Helpers.TicketType.Ticket3D:
                        Tickets3D += 1;
                        nombre = "Boleto 3D";
                        precio = 60;
                        break;
                    case CineTaquilla.Helpers.TicketType.VipTicket:
                        VipTicket += 1;
                        nombre = "Boleto VIP";
                        precio = 70;
                        break;
                    default:
                        normalTickets += 1;
                        nombre = "Boleto Normal";
                        precio = 50;
                        break;
                }
                var producto = new Producto
                {
                    ProductId = 5,
                    Name = nombre,
                    Total = precio,
                    Quantity = 1,
                    Tipo = nombre,
                    Amount = precio
                };
                productos.Add(producto);
            }
            normalTicketsAmount = normalTickets * 50;
            Tickets3DAmount = Tickets3D * 60;
            VipTicketAmount = VipTicket * 70;
            TotalAmount = normalTicketsAmount + Tickets3DAmount + VipTicketAmount;
            daySales.NomalTicketsAmount = normalTicketsAmount;
            daySales.Tickets3DAmount = Tickets3DAmount;
            daySales.Tickets3DVIPAmount = VipTicketAmount;
            daySales.NomalTicketsCount = normalTickets;
            daySales.Tickets3DCount = Tickets3D;
            daySales.TicketsVIPCount = VipTicket;
            daySales.SaleDate = sales.FirstOrDefault().SaleDate;
            daySales.TotalAmount = TotalAmount;

            //await _sc.RegisterDaySales(daySales);

            
            if (await _sc.RegisterDaySales(daySales))
            {
                try
                {
                    var req = new RestRequest("http://cinefinanzas.gear.host/api/Finance/IncomeRegister")
                                        {
                        Method = Method.POST,
                        RequestFormat = DataFormat.Json
                    };

                    var model = new {
                        departmentKey = 1,
                        date = sales.FirstOrDefault().SaleDate,
                        productList = new List<Producto>(productos),
                        total = productos.Sum( x => x.Total),
                    };

                    req.AddJsonBody(model);
                    var resp = await _client.ExecutePostTaskAsync(req);
                    if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                        return Ok("Se guardón correctamente");
                    return BadRequest("Se realizó el corte pero no se guardaron los registros en la base de datos de finanzas");
                }
                catch (Exception ex)
                {

                }
            }
            return BadRequest("No se pudo realizar correctamente el corte");

        }

    }
}
