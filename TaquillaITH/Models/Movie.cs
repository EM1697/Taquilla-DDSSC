using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaquillaITH.Models.DTO;

namespace TaquillaITH.Models
{
    public class Movie : Model
    {
        public string Name { get; set; }
        public string Schedule { get; set; }
        public string Genre { get; set; }
        public int Num_Sala { get; set; }
        public int RunningTime { get; set; }
        public string Synopsis { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_final { get; set; }

        //New Fields
        [NotMapped]
        public string pelicula { get; set; }
        [NotMapped]
        public List<string> horarios { get; set; }
        [NotMapped]
        public int sala { get; set; }
        [NotMapped]
        public int duracion { get; set; }
        [NotMapped]
        public string sinopsis { get; set; }
        [NotMapped]
        public string genero { get; set; }
        //[NotMapped]
        //public List<Boleto> precioBoletos { get; set; }
        [NotMapped]
        public string photoUrl { get; set; }
        [NotMapped]
        public DateTime fecha_inicio { get; set; }
        [NotMapped]
        public DateTime fecha_final { get; set; }
    }
}
