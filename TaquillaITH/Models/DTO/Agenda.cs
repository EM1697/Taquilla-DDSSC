using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models.DTO
{
    public class Agenda : Model
    {
        public string Titulo { get; set; }
        public int Duracion { get; set; }
        public string Sinopsis { get; set; }
        public string Categoria { get; set; }
        public string Horarios { get; set; }
        public int Sala { get; set; }
    }
}
