using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models
{
    public class Movie : Model
    {
        public string Name { get; set; }
        public string Schedule { get; set; }
        public string Genre { get; set; }
        public int RunningTime { get; set; }
        public int Num_Sala { get; set; }
        public string Synopsis { get; set; }
        public string PhotoUrl { get; set; }
    }
}
