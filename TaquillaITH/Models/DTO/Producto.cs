using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models.DTO
{
    public class Producto : Model
    {
        public int Sala { get; set; }
        public string Hora { get; set; }
        public string Name { get; set; }
        public string Pelicula { get; set; }
        public string Tipo { get; set; }
        public double Precio { get; set; }
    }
}
