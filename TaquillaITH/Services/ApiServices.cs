using TaquillaITH.Data;
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

        public List<List<Seat>> GetShowSeats()
        {
            try
            {
                var Seats = _db.Seats.ToList();
                var OrderedSeats = Seats.GroupBy(x => x.Name.Length == 2 ? x.Name.Substring(1,1) : x.Name.Substring(2,1)).Select(x=>x.ToList()).ToList();
                return OrderedSeats;
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
                var data =  (from m in _db.Movies
                            join s in _db.Shows on m.Id equals s.MovieId
                            join r in _db.TheatreRooms on s.TheatreRoomId equals r.Id
                            where (s.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false)
                            select new ShowTimesViewModel()
                            {
                                nombre = m.Name,
                                horario = m.Schedule,
                                sala = r.Name,
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
