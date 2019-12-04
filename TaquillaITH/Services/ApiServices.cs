﻿using TaquillaITH.Data;
using TaquillaITH.Models;
using TaquillaITH.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Services
{
    public class ApiServices : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApiServices(ApplicationDbContext db)
        {
            _db = db;
        }

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
                    Seats.FirstOrDefault(x=>x.Name == item).Occupied = true;

                return Seats.GroupBy(x => x.Name.Length == 2 ? x.Name.Substring(1, 1) : x.Name.Substring(2, 1)).Select(x => x.ToList()).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Show GetShow(int idSala, DateTime Horario)
        {
            try
            {
                return _db.Shows.FirstOrDefault(x => x.TheatreRoomId == idSala && x.ShowTime == Horario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
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

        public List<ShowTimesViewModel> GetShowTimes()
        {
            try
            {
                var data =  (from m in _db.Movies
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
                                duracion = m.RunningTime
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DaySalesViewModel> GetDaySales(string dateF){
            DateTime date = DateTime.ParseExact(dateF, "yyyy-MM-dd HH:mm:ss", null);
            try
            {
                var data =  (from s in _db.Sales
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
                                RunningTime = m.RunningTime
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
