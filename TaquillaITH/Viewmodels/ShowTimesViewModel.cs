using System;
using TaquillaITH.Models;
using TaquillaITH.Data;
using System.Collections.Generic;
using System.Linq;

namespace TaquillaITH.ViewModels
{
    public class ShowTimesViewModel
    {
        public string nombre {get; set;}
        public string horario {get; set;}
        public List<string> horarios {get; set;}
        public string sala {get; set;}
        public string genero {get; set;}
        public string duracion {get; set;}
        public string sinopsis {get; set;}
    }
}
