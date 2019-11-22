using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTaquilla.Helpers;

namespace TaquillaITH.Models
{
    public class Seat : Model
    {
        public string Name { get; set; }

        public SeatStatus Status { get; set; }

    }
}

