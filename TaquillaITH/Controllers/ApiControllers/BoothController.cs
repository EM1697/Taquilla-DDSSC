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

        #region Variables, context, etc
        private readonly ApiServices _apiServices;
        public RestClient _client;
        public BoothController(ApiServices apiServices)
        {
            _apiServices = apiServices;
            _client = new RestClient();

        }
        #endregion

        #region HTTP Get
        [HttpGet("GetShowSeats")]
        public async Task<IActionResult> GetShowSeats(int idSala, string Horario)
        {
            try
            {
                var model = _apiServices.GetShowSeats(idSala, Horario);

                if (model == null)
                    return BadRequest("No hay ningna funcion disponible en esta sala a esta hora.");

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
                #region Get Movies
                //Llamado api a Renee
                var req = new RestRequest("http://peliculaapi.gearhostpreview.com/index.php/Cartelera/agenda")
                {
                    Method = Method.GET,
                    RequestFormat = DataFormat.Json
                };

                //Obtener cartelera de gestion de peliculas
                var resp = await _client.ExecuteGetTaskAsync(req);
                List<Movie> Movies = new List<Movie>();
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var model2 = JsonConvert.DeserializeObject<Pelicula>(resp.Content);
                    if (model2 != null && model2.Agenda.Any())
                    {
                        foreach (var movie in model2.Agenda)
                        {
                            Random rnd = new Random();
                            int random = rnd.Next(9, 20);

                            var peli = new Movie
                            {
                                Name = movie.Titulo,
                                Schedule = movie.Horarios ?? $"{random}:00",
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
                    else
                        return BadRequest("Hubo un error al momento de actualizar el catalogo de peliculas");
                }
                #endregion

                #region Shows
                //Crear nuevos shows de peliculas
                DateTime date = DateTime.Now;
                List<Show> newShows = new List<Show>();
                if (date.DayOfWeek.ToString() == "Sunday" || date.DayOfWeek.ToString() == "Monday")
                {
                    List<Movie> newMovies = new List<Movie>();
                    newMovies = _apiServices.GetMovies(); //Lista de peliculas nuevas
                    foreach (Movie movie in newMovies) //Recorrer cada pelicula
                    {
                        List<String> newHorarios = movie?.Schedule?.Replace(" ", string.Empty).Split(',').ToList() ?? new List<string> { "12:00" };
                        foreach (var hora in newHorarios) //Cada horario por pelicula
                        {
                            DateTime date2 = date;
                            for (int i = 0; i < 7; i++)
                            {
                                var show = new Show() //Crear nuevo show
                                {
                                    MovieId = movie.Id,
                                    TheatreRoomId = movie.Num_Sala,
                                    ShowTime = Convert.ToDateTime($"{date2.Year}-{date2.Month}-{date2.Day} {hora}"),
                                    UsedSeats = ""
                                };
                                newShows.Add(show);
                                date2 = date2.AddDays(1);
                            }
                        }
                    }
                    await _apiServices.UpdateNewShows(newShows);
                }
                //Get de Shows
                var model = _apiServices.GetShowTimes();
                foreach (var data in model)
                {
                    Random rnd = new Random();
                    int random = rnd.Next(9, 20);
                    data.horarios = data?.horario?.Replace(" ", string.Empty).Split(',').ToList() ?? new List<string> { $"{random}:00" };
                }
                #endregion

                #region Promociones
                //Llamado api de Promociones - Julio
                var promoRequest = new RestRequest("http://cinefinanzas.gear.host/api/Finance/Promotions")
                {
                    Method = Method.GET,
                    RequestFormat = DataFormat.Json
                };
                promoRequest.AddQueryParameter("DepartmentKey", "1");

                //Obtener cartelera de gestion de peliculas
                var promoResponse = await _client.ExecuteGetTaskAsync(promoRequest);
                List<PromotionsViewModel> Promotions = new List<PromotionsViewModel>();
                if (promoResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var ApiPromos = JsonConvert.DeserializeObject<PromotionsListViewModel>(promoResponse.Content);
                    foreach (var item in ApiPromos.Promotions)
                        Promotions.Add(item);
                }
                else
                    return BadRequest("Error al mostrar las promociones.");
                #endregion

                var JsonReturn = Movies.Select(x=> new 
                {
                    pelicula = x.Name,
                    horarios = x?.Schedule?.Replace(" ", string.Empty).Split(',').ToList() ?? new List<string> { "12:00" },
                    sala = x.Num_Sala,
                    duracion = x.RunningTime,
                    sinopsis = x.Synopsis,
                    genero = x.Genre,
                    precioBoletos = new
                    {
                        boletoNormal = Promotions[0].Price,
                        boleto3D = Promotions[1].Price,
                        boletoVIP = Promotions[2].Price
                    },
                    photoUrl = x.PhotoUrl
                });

                return Ok(JsonReturn);
            }
            catch (Exception ex)
            {
                return BadRequest("Obtener el catálogo de asientos falló debido a: " + ex);
            }
        }

        #endregion

        #region HTTP Post
        [HttpPost("SelectShowSeats")]
        public async Task<IActionResult> SelectedSeats(ShowTimeSeatsViewModel seats)
        {
            try
            {
                var Schedule = Convert.ToDateTime(seats.Horario);
                var Show = _apiServices.GetShow(seats.IdSala, Schedule);
                
                //if (string.IsNullOrEmpty(Show.UsedSeats))
                //{
                //    Show.UsedSeats = "";
                //}

                if (seats.AsientosUsados != null && seats.AsientosUsados.Any())
                {
                    foreach (var item in seats.AsientosUsados)
                    {
                        if (string.IsNullOrEmpty(Show?.UsedSeats ?? new string("")))
                            Show.UsedSeats = item.ToUpper();
                        else
                            Show.UsedSeats += $",{item.ToUpper()}";
                    }
                }
                else
                    seats.AsientosUsados = new List<string>();

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

        [HttpPost("PostTicketSale")]
        public async Task<IActionResult> PostTicketSale(TicketSaleViewModel venta)
        {
            try
            {
                venta.Puntos = Convert.ToInt32(Math.Truncate(venta.Total * 1.10));

                //Membresia
                var membershipRequest = new RestRequest("https://membresiascomplejo.azurewebsites.net/api/membresias/solicitardatos")
                {
                    Method = Method.GET,
                    RequestFormat = DataFormat.Json
                };
                membershipRequest.AddQueryParameter("id", "9");
                var membershipResponse = await _client.ExecuteGetTaskAsync(membershipRequest);

                if (membershipResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var Membership = JsonConvert.DeserializeObject<Membresia>(membershipResponse.Content);

                    //Generar Puntos
                    var pointsRequest = new RestRequest("https://membresiascomplejo.azurewebsites.net/api/membresias/GenerarPuntos")
                    {
                        Method = Method.POST,
                        RequestFormat = DataFormat.Json
                    };

                    var pointsModel = new
                    {
                        Id_Membresia = 9,
                        Id_Punto_Venta = 1,
                        Puntos_Generados = venta.Puntos
                    };

                    pointsRequest.AddJsonBody(pointsModel);
                    var pointsResponse = await _client.ExecutePostTaskAsync(pointsRequest);

                    if (pointsResponse.StatusCode != System.Net.HttpStatusCode.OK)
                        return BadRequest("Ocurrio un error al momento de generar los puntos");

                    //Banquito
                    var bankRequest = new RestRequest("http://138.68.6.44:8000/api/transacciones/transferencias/")
                    {
                        Method = Method.POST,
                        RequestFormat = DataFormat.Json,
                    };

                    var bankModel = new
                    {
                        tarjeta_origen = "5050543614668653",
                        tarjeta_destino = "5050464168614617",
	                    cvv = "078",
	                    fecha_vencimiento = "12/21",
	                    monto = venta.Total
                    };

                    bankRequest.AddJsonBody(bankModel);
                    var bankResponse = await _client.ExecutePostTaskAsync(bankRequest);

                    if (bankResponse.StatusCode != System.Net.HttpStatusCode.OK)
                        return BadRequest("Ocurrio un error al momento de la transaccion");

                    //Banquito

                    bool Registred = await _apiServices.RegisterSale(venta);

                    if (!Registred)
                        return BadRequest("No se pudo guardar la venta debido a un error en el servidor");

                    return Ok();
                }
                else
                    return BadRequest("Error al cargar la membresia");
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

        #endregion
    }
}