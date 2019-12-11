using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models.DTO
{
    public class Pelicula   : Model
    {
       public string Mensaje { get; set; }
       public int total_registros { get; set; }
       public List<Agenda> Agenda { get; set; }
    }
}
