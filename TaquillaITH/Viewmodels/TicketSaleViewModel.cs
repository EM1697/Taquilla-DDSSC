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
        public string Numero_Venta { get; set; }
        public string Codigo_Cliente { get; set; }
        public int Numero_Transaccion { get; set; }
        public int Puntos { get; set; }
        public string Fecha { get; set; }
        public string Nombre_Cliente { get; set; }
        public List<Producto> Productos { get; set; }
        public float Total { get; set; }
    }
}
