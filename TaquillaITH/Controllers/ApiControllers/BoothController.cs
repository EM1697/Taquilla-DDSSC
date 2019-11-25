﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaquillaITH.Services;
using Microsoft.AspNetCore.Cors;
using TaquillaITH.Models;

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

        [HttpGet("GetShowSeats")]
         public async Task<IActionResult> GetShowSeats()
        {
            try
           {
               var model = _apiServices.GetShowSeats();
               return Ok(model);
           }
           catch (Exception ex)
           {
               return BadRequest("Obtener el catálogo de asientos falló debido a: " + ex.Message);
           }
        }

    }
}