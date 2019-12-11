using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTaquilla.Helpers;

namespace TaquillaITH.Models
{
    public class Sale : Model
    {
        public virtual Movie Movie { get; set; }
        public string MovieName { get; set; }
        public string Time { get; set; }
        public int? UserId { get; set; }
        public TicketType TipoBoletoId { get; set; }
        public DateTime SaleDate { get; set; }
        public virtual Payment Payment { get; set; }
        public int? DaySalesId { get; set; }
        public virtual DaySales DaySales { get; set; }
    }
}

