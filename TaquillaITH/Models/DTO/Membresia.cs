using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models.DTO
{
    public class Membresia : Model
    {
        public int statusCode { get; set; }
        public int id_Membresia { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public string porcentaje { get; set; }
        public decimal puntos { get; set; }
    }
}
