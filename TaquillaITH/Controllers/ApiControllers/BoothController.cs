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
        public async Task<IActionResult> GetShowTimes(string fecha22)
        {
            try
            {
                DateTime pitote = new DateTime();
                if (!string.IsNullOrEmpty(fecha22))
                    pitote = Convert.ToDateTime(fecha22);
                else
                    pitote = DateTime.Now;

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
                                PhotoUrl = movie.Portada,
                                Fecha_inicio = movie.Fecha_inicio,
                                Fecha_final = movie.Fecha_final
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
                List<Movie> newMovies = new List<Movie>();
                    newMovies = _apiServices.GetMovies(pitote); //Lista de peliculas nuevas
                    foreach (Movie movie in newMovies) //Recorrer cada pelicula
                    {
                        List<String> newHorarios = movie?.Schedule?.Replace(" ", string.Empty).Split(',').ToList() ?? new List<string> { "12:00" };
                        foreach (var hora in newHorarios) //Cada horario por pelicula
                        {
                            DateTime fecha = movie.Fecha_inicio;

                            //for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                            //    yield return day;

                            foreach (DateTime day in EachDay(movie.Fecha_inicio, movie.Fecha_final))
                            {
                                var show = new Show() //Crear nuevo show
                                {
                                    MovieId = movie.Id,
                                    TheatreRoomId = movie.Num_Sala,
                                    ShowTime = Convert.ToDateTime($"{day.Year}-{day.Month}-{day.Day} {hora}"),
                                    UsedSeats = ""
                                };
                                newShows.Add(show);
                                //fecha = fecha.AddDays(1);
                            }
                        }
                    }
                    await _apiServices.UpdateNewShows(newShows);

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
                //Llamado api de Promociones - Julio Fuentes
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
                
                var JsonReturn = newMovies.Select(x => new 
                {
                    pelicula = x.Name,
                    horarios = x?.Schedule?.Replace(" ", string.Empty).Split(',').ToList() ?? new List<string> { "12:00" },
                    sala = x.Num_Sala,
                    duracion = x.RunningTime,
                    sinopsis = x.Synopsis,
                    genero = x.Genre,
                    precioBoletos = new
                    {
                        boletoNormal = 50 - 50 * Promotions[0].Percentaje/100, 
                        boleto3D = 60 - 60 * Promotions[1].Percentaje/100,
                        boletoVIP = 70 - 70 * Promotions[0].Percentaje/100
                    },
                    photoUrl = x.PhotoUrl,
                    fecha_inicio = x.Fecha_inicio,
                    fecha_final = x.Fecha_final
                });

                return Ok(JsonReturn);
            }
            catch (Exception ex)
            {
                return BadRequest("Obtener el catálogo de asientos falló debido a: " + ex);
            }
        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        [HttpGet("GetMembership")]
        public async Task<IActionResult> GetMembership(string id)
        {
            //Membresia - Carlos Salido
            var membershipRequest = new RestRequest("https://membresiascomplejo.azurewebsites.net/api/membresias/solicitardatos")
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };
            membershipRequest.AddQueryParameter("id", id);
            var membershipResponse = await _client.ExecuteGetTaskAsync(membershipRequest);
            var model = JsonConvert.DeserializeObject<Membresia>(membershipResponse.Content);

            if (membershipResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var membership = new Membresia
                {
                    statusCode = model.statusCode,
                    id_Membresia = model.id_Membresia,
                    nombre = model.nombre,
                    password = model.password,
                    porcentaje = model.porcentaje,
                    puntos = model.puntos
                };
                return Ok(membership);
            }
        return BadRequest("Error al cargar la membresia");
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

        [HttpPost("PostMembershipPoints")]
        public async Task<IActionResult> PostMembershipPoints(string id_Membresia, string puntos)
        {
            try
            {
                //Generar Puntos
                var pointsRequest = new RestRequest("https://membresiascomplejo.azurewebsites.net/api/membresias/GenerarPuntos")
                {
                    Method = Method.POST,
                    RequestFormat = DataFormat.Json
                };

                var Concha = Math.Truncate(Convert.ToDecimal(puntos));
                var Panocha = Convert.ToInt32(id_Membresia);

                pointsRequest.AddJsonBody(new 
                {
                    Id_Membresia = Panocha,
                    Id_Punto_Venta = 1,
                    Puntos_Generados = Concha
                });
                var pointsResponse = await _client.ExecutePostTaskAsync(pointsRequest);

                if (pointsResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    return BadRequest("Ocurrio un error al momento de generar los puntos");
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

        [HttpPost("PostTicketSale")]
        public async Task<IActionResult> PostTicketSale(TicketSaleViewModel venta)
        {
            try
            {
                if (venta.helper)
                {
                    if (venta.usedCredit)
                    {
                        if (venta.pointFlag)
                            venta.Total -= venta.Puntos;

                        //Banquito
                        var bankRequest = new RestRequest("http://138.68.6.44:8000/api/transacciones/transferencias/")
                        {
                            Method = Method.POST,
                            RequestFormat = DataFormat.Json,
                        };

                        var bankModel = new
                        {
                            venta.tarjeta_origen,
                            tarjeta_destino = "5050464168614617",
                            venta.cvv,
                            venta.fecha_vencimiento,
                            monto = venta.Total
                        };
                        Sale sale = new Sale();

                        bankRequest.AddJsonBody(bankModel);
                        var bankResponse = await _client.ExecutePostTaskAsync(bankRequest);
                        if (bankResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var Transaction = new Transaction
                            {
                                SenderFourDigits = $"XXXX XXXX XXXX {bankModel.tarjeta_origen.Substring(bankModel.tarjeta_origen.Length - 4)}",
                                ReceiverFourDigits = $"XXXX XXXX XXXX {bankModel.tarjeta_destino.Substring(bankModel.tarjeta_destino.Length - 4)}",
                                Amount = Convert.ToDecimal(venta.Total),
                                TransactionDate = DateTime.Now,
                                CreationDate = DateTime.Now,
                                LastUpdate = DateTime.Now
                            };
    
                            var TransactionId = await _apiServices.GenerateTransaction(Transaction);
                            if (TransactionId == 0)
                                return BadRequest("Ocurrio un error al momento de guardar la transaccion");
                            venta.TransactionId = TransactionId;
                        }
                        else
                            return BadRequest("Ocurrio un error al momento de la transaccion con el banco");
                    }

                    var seatsRequest = new RestRequest("https://taquilla2.gear.host/api/booth/SelectShowSeats/")
                    {
                        Method = Method.POST,
                        RequestFormat = DataFormat.Json,
                    };

                    var seatsModel = new
                    {
                        IdSala = venta.sala,
                        Horario = venta.schedule,
                        AsientosUsados = venta.UsedSeats
                    };

                    seatsRequest.AddJsonBody(seatsModel);
                    var seatsResponse = await _client.ExecutePostTaskAsync(seatsRequest);

                    if (seatsResponse.StatusCode != System.Net.HttpStatusCode.OK)
                        return BadRequest("Ocurrio un error al momento reservar los asientos");

                }

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

        #endregion
    }
}