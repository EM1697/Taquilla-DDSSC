using System;
using TaquillaITH.Models;
using TaquillaITH.Data;
using System.Collections.Generic;
using System.Linq;
using CineTaquilla.Helpers;
using System.ComponentModel.DataAnnotations;
using TaquillaITH.Models.DTO;

namespace TaquillaITH.ViewModels
{
    public class EverythingViewModel
    {
        //ShowTime
        public string nombre { get; set; }
        public string horario { get; set; }
        public List<string> horarios { get; set; }
        public string sala { get; set; }
        public string genero { get; set; }
        public string duracion { get; set; }
        public string sinopsis { get; set; }
        public string photoUrl { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        //ShowTime


    }
}
