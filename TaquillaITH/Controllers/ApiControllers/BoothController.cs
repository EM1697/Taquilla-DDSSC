using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaquillaITH.Services;

namespace TaquillaITH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoothController : Controller
    {
        private readonly ApiServices _apiServices;
        public BoothController(ApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        // [HttpGet]
        // public IEnumerable<Products> ListAllProducts()
        // {
        //     return products;
        // }

         [HttpGet("HolaMundo")]
        public string holaMundo()
        {
            return "Hello World";
        }

        //[HttpGet("GetTicketInfo")]
        ////metodo de api para registrar los ingresos de cortes de los departamentos
        //public Task<IActionResult> IncomeRegister()
        //{
        //    //try
        //    //{
        //    //    return Ok("");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return BadRequest("El registro de corte en finanzas falló debido a: " + ex.Message);
        //    //}
        //}
    }
}