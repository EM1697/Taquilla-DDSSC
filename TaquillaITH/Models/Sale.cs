using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTaquilla.Helpers;

namespace TaquillaITH.Models
{
    public class Sale : Model
    {
        public string Name { get; set; }
        public string Time { get; set; }

        public int? UserId { get; set; }
        public List<TheatreRoom> Seats { get; set; }
        public TicketType TipoBoletoId { get; set; }
    }
}

