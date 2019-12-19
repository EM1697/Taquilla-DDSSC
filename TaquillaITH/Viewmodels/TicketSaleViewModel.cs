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
    public class TicketSaleViewModel
    {
        public bool usedCredit { get; set; }
        public bool pointFlag { get; set; }

        public int cash { get; set; }
        public int credit { get; set; }


        public bool helper { get; set; }
        public int sala { get; set; }
        public string schedule { get; set; }
        public string hora { get; set; }
        public List<string> UsedSeats { get; set; }

        public string cvv { get; set; }
        public string mes { get; set; }
        public string ano { get; set; }
        public string tarjeta_origen { get; set; }
        public string fecha_vencimiento { get => $"{mes}/{ano}"; }

        public string Numero_Venta { get; set; }
        public string Codigo_Cliente { get; set; }
        public int Numero_Transaccion { get; set; }
        public int Puntos { get; set; }
        public int TransactionId { get; set; }
        public string Fecha { get; set; }
        public string Nombre_Cliente { get; set; }
        public List<Producto> Productos { get; set; }
        public float Total { get; set; }
    }
}
