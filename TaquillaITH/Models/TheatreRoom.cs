using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaquillaITH.Models
{
    public class TheatreRoom : Model
    {
        public string Name { get; set; }
        [NotMapped]
        public List<string> Seats { get; set; }
    }
}