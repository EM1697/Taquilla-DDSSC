using System.ComponentModel;

namespace TaquillaITH.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Ocupado")]
        public bool Occupied { get; set; }
    }
}

