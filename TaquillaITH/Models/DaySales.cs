using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models
{
    public class DaySales : Model
    {
        public int NomalTicketsCount { get; set; }
        public decimal NomalTicketsAmount { get; set; }
        public int Tickets3DCount { get; set; }
        public decimal Tickets3DAmount { get; set; }
        public int TicketsVIPCount { get; set; }
        public decimal Tickets3DVIPAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
