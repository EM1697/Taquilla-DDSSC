using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models
{
    public class DaySales : Model
    {
        public int MovieId { get; set; }
        public string Description { get; set; } 

        public int NomalTicketsCount { get; set; }
        public decimal NomalTicketsAmount { get; set; }

        public int Tickets3DCount { get; set; }
        public decimal Tickets3DAmount { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
