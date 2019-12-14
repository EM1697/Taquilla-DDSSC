using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaquillaITH.Services;
using Microsoft.AspNetCore.Cors;
using TaquillaITH.Models;
using TaquillaITH.Models.DTO;
using TaquillaITH.ViewModels;
using RestSharp;
using Newtonsoft.Json;

namespace TaquillaITH.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class BoothController : Controller
    {
        private readonly ApiServices _apiServices;

        public RestClient _client;

        public BoothController(ApiServices apiServices)
        {
            _apiServices = apiServices;
            _client = new RestClient();

        }

        [HttpGet("GetShowSeats")]
        public async Task<IActionResult> GetShowSeats(int idSala, string Horario)
        {
            try
            {
                var model = _apiServices.GetShowSeats(idSala, Horario);

                if (model == null)
                    return BadRequest("Obtener el catálogo de asientos falló");

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest("Obtener el catálogo de asientos falló debido a: " + ex.Message);
            }
        }

        [HttpGet("GetShowTimes")]
        public async Task<IActionResult> GetShowTimes()
        {
            try
            {
                //Llamado api a Rene
                var req = new RestRequest("http://peliculaapi.gearhostpreview.com/index.php/Cartelera/agenda")
                {
                    Method = Method.GET,
                    RequestFormat = DataFormat.Json
                };

                var resp = await _client.ExecuteGetTaskAsync(req);
                List<Movie> Movies = new List<Movie>();
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model2 = JsonConvert.DeserializeObject<Pelicula>(resp.Content);
                    if (model2 != null && model2.Agenda.Any())
                    {
                        var updatedMovies = await _apiServices.DeleteOldMovies();
                        if (updatedMovies)
                        {
                            foreach (var movie in model2.Agenda)
                            {
                                var peli = new Movie
                                {
                                    Name = movie.Titulo,
                                    Schedule = movie.Horarios,
                                    Genre = movie.Categoria,
                                    RunningTime = movie.Duracion,
                                    Synopsis = movie.Sinopsis,
                                    Num_Sala = movie.Num_Sala,
                                    PhotoUrl = movie.Portada
                                };
                                Movies.Add(peli);
                            }
                            var examen = await _apiServices.UpdateMovies(Movies);
                            if (!examen)
                                return BadRequest("Error al momento de actualizar las peliculas recientes");
                        }
                    }
                    else
                        return BadRequest("Hubo un error al momento de actualizar el catalogo de peliculas");
                }

                var algo = await _apiServices.UpdateShows(Movies);

                //Get de Shows
                var model = _apiServices.GetShowTimes();
                foreach (var data in model)
                    data.horarios = data?.horario?.Replace(" ", string.Empty).Split(',').ToList() ?? new List<string>{"12:00"};

                var movies = model.Select(x => new 
                {
                    pelicula = x.nombre,
                    x.horarios, 
                    x.sala,
                    x.duracion, 
                    x.sinopsis,
                    x.genero,
                    precioBoletos = new 
                    { 
                        boletoNormal = 50, boleto3D = 60, boletoVIP = 70 
                    },
                    x.photoUrl
                });
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest("Obtener el catálogo de asientos falló debido a: " + ex);
            }
        }

        //Post
        [HttpPost("SelectShowSeats")]
        public async Task<IActionResult> SelectedSeats(ShowTimeSeatsViewModel seats)
        {
            try
            {
                var Schedule = Convert.ToDateTime(seats.Horario);
                var Show = _apiServices.GetShow(seats.IdSala, Schedule);

                foreach (var item in seats.AsientosUsados)
                {
                    if (string.IsNullOrEmpty(Show.UsedSeats))
                        Show.UsedSeats += item.ToUpper();
                    else
                        Show.UsedSeats += $",{item.ToUpper()}";
                }

                bool ShowUpdated = await _apiServices.UpdateShow(Show);
                if (ShowUpdated)
                    return Ok();
                else
                    return BadRequest("No se puedieron guardar los asientos");
            }
            catch (Exception ex)
            {
                return BadRequest("No se puedieron guardar los asientos debido a " + ex);
            }
        }

        //Post
        [HttpPost("PostTicketSale")]
        public async Task<IActionResult> PostTicketSale(TicketSaleViewModel venta)
        {
            try
            {
                bool Registred = await _apiServices.RegisterSale(venta);

                if (!Registred)
                    return BadRequest("No se pudo guardar la venta debido a un error en el servidor");

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo guardar la venta debido a " + ex);
            }
        }

        [HttpPost("SavePurchase")]
        public async Task<IActionResult> SavePurchase(Sale sale)
        {
            try
            {
                var model = await _apiServices.SavePurchase(sale);
                if (!model)
                    return BadRequest("Hubo un error al momento de guardar la venta, por favor inténtelo después");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Hubo un error al momento de guardar la venta, por favor intentelo despues" + ex.Message);
            }
        }
    }
}