using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CineTaquilla.Helpers;

namespace TaquillaITH.Models
{
    public class TheatreRoom : Model
    {
        public string Name { get; set; }
        public Dictionary<string, SeatStatus> Seats { get; set; }
    }
}