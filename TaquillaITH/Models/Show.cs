using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models
{
    public class Show : Model
    {
        public int MovieId {get; set;}
        public virtual Movie Movie {get; set;}
        public int TheatreRoomId {get; set;}
        public virtual TheatreRoom TheatreRoom {get; set;}
        public DateTime ShowTime { get; set; }
        public string UsedSeats { get; set; }
    }
}
