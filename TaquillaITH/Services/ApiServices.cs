using TaquillaITH.Data;
using TaquillaITH.Models;
using TaquillaITH.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTaquilla.Helpers;

namespace TaquillaITH.Services
{
    public class ApiServices : Controller
    {
        #region DBContext
        private readonly ApplicationDbContext _db;

        public ApiServices(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get Methods
        public List<List<Seat>> GetShowSeats(int idSala, string Horario)
        {
            try
            {
                var Schedule = Convert.ToDateTime(Horario);

                if (idSala <= 0)
                    return null;

                var Show = GetShow(idSala, Schedule);
                var Seats = _db.Seats.ToList();
                var UsedSeats = Show.UsedSeats.Split(",").ToList();

                foreach (var item in UsedSeats)
                {
                    if (item != "")
                        Seats.FirstOrDefault(x => x.Name == item).Occupied = true;
                }

                return Seats.GroupBy(x => x.Name.Length == 2 ? x.Name.Substring(1, 1) : x.Name.Substring(2, 1)).Select(x => x.ToList()).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Movie> GetMovies()
        {
            return _db.Movies.Where(x => x.IsDeleted == false).ToList();
        }

        public Show GetShow(int idSala, DateTime Horario)
        {
            try
            {

                var Shous = _db.Shows.FirstOrDefault(x => x.TheatreRoomId == idSala && x.ShowTime.Date == Horario.Date && x.ShowTime.Hour == Horario.Hour);
                return Shous;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ShowTimesViewModel> GetShowTimes()
        {
            try
            {
                var data = (from m in _db.Movies
                            join s in _db.Shows on m.Id equals s.MovieId
                            join r in _db.TheatreRooms on s.TheatreRoomId equals r.Id
                            where (s.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false)
                            select new ShowTimesViewModel()
                            {
                                nombre = m.Name,
                                horario = m.Schedule,
                                sala = r.Name,
                                genero = m.Genre,
                                sinopsis = m.Synopsis,
                                duracion = m.RunningTime.ToString(),
                                photoUrl = m.PhotoUrl
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DaySalesViewModel> GetDaySales(string dateF)
        {
            DateTime date = DateTime.ParseExact(dateF, "yyyy-MM-dd HH:mm:ss", null);
            try
            {
                var data = (from s in _db.Sales
                            join p in _db.Payments on s.Payment.Id equals p.Id
                            join m in _db.Movies on s.Movie.Id equals m.Id
                            where (s.IsDeleted == false && p.IsDeleted == false && m.IsDeleted == false && s.SaleDate.Day == date.Day && s.SaleDate.Month == date.Month && s.SaleDate.Year == date.Year)
                            select new DaySalesViewModel()
                            {
                                UserId = s.UserId,
                                TipoBoletoId = s.TipoBoletoId,
                                SaleDate = s.SaleDate,
                                Cash = p.Cash,
                                CreditCard = p.CreditCard,
                                RewardPoints = p.RewardPoints,
                                TotalAmount = p.Cash + p.CreditCard + p.TotalAmount,
                                Name = m.Name,
                                Schedule = m.Schedule,
                                RunningTime = m.RunningTime.ToString()
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Update Methods
        public async Task<bool> UpdateShow(Show show)
        {
            try
            {
                _db.Shows.Update(show);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> UpdateMovies(List<Movie> movieList)
        {
            try
            {
                List<Movie> finalList = new List<Movie>();
                var alreadyInDbMovies = _db.Movies.Where(x => !x.IsDeleted).ToList();

                if (!alreadyInDbMovies.Any())
                {
                    _db.Movies.AddRange(movieList);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    foreach (var movie in movieList)
                    {
                        bool flag = false;

                        foreach (var inDbMovie in alreadyInDbMovies)
                        {
                            if (movie.Name == inDbMovie.Name)
                            {
                                flag = false;
                                break;
                            }
                            else
                            {
                                flag = true;
                            }
                        }

                        if (flag)
                            finalList.Add(movie);
                    }

                    _db.Movies.AddRange(finalList);
                    await _db.SaveChangesAsync();
                    return true;

                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateNewShows(List<Show> showsList)
        {
            try
            {
                List<Show> finalList = new List<Show>();
                var alreadyInDbShows = _db.Shows.Where(x => !x.IsDeleted).ToList();

                if (!alreadyInDbShows.Any())
                {
                    _db.Shows.AddRange(showsList);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    foreach (var show in showsList)
                    {
                        bool flag = false;

                        foreach (var inDbShow in alreadyInDbShows)
                        {
                            if (show.TheatreRoomId == inDbShow.TheatreRoomId && show.ShowTime.Day == inDbShow.ShowTime.Day && show.ShowTime.Hour == inDbShow.ShowTime.Hour)
                            {
                                flag = false;
                                break;
                            }
                            else
                            {
                                flag = true;
                            }
                        }

                        if (flag)
                            finalList.Add(show);
                    }

                    _db.Shows.AddRange(finalList);
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    
        #endregion

        #region Other Methods
        public async Task<bool> SavePurchase(Sale sale)
        {
            try
            {
                _db.Sales.Add(sale);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterDaySales(DaySales sales)
        {
            try
            {
                _db.DaySales.Add(sales);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RegisterSale(TicketSaleViewModel venta)
        {
            try
            {
                var MovieSchedule = Convert.ToDateTime(venta.Fecha);
                List<Sale> ventas = new List<Sale>();

                foreach (var item in venta.Productos)
                {
                    var VentaCrack = new Sale
                    {
                        CreationDate = DateTime.Now,
                        LastUpdate = DateTime.Now,
                        SaleDate = DateTime.Now,
                        Time = item.Hora,
                        TipoBoletoId = GetTicketType(item.Tipo),
                        Payment = new Payment
                        {
                            Cash = Convert.ToDecimal(item.Precio)
                        },
                        MovieName = item.Pelicula
                    };
                    ventas.Add(VentaCrack);
                }

                _db.Sales.AddRange(ventas);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private TicketType GetTicketType(string Boleto)
        {
            switch (Boleto)
            {
                case "boletoVIP":
                    return TicketType.VipTicket;
                case "boleto3D":
                    return TicketType.Ticket3D;
                case "boletoNormal":
                    return TicketType.NormalTicket;
                default:
                    return TicketType.NormalTicket;
            }
        }

        #endregion
    }
}