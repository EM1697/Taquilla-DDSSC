using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTaquilla.Helpers;

namespace TaquillaITH.Models
{
    public class Ticket : Model
    {
        public int BoletoId { get; set; }
        public int Sala { get; set; }
        public string Hora { get; set; }
        public string NombrePelicula { get; set; }
        public string Asiento { get; set; }
        public TicketType TipoBoletoId { get; set; }
        public int Price { get; set; }
    }
}
