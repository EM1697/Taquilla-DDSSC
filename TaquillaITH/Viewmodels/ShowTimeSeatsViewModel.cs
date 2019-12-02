using System;
using TaquillaITH.Models;
using System.Collections.Generic;

namespace TaquillaITH.ViewModels
{
    public class ShowTimeSeatsViewModel
    {
        public int IdSala { get; set; }
        public string Horario { get; set; }
        public List<string> AsientosUsados { get; set; }
    }
}
