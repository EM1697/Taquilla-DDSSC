using System;
using TaquillaITH.Models;
using TaquillaITH.Data;
using System.Collections.Generic;
using System.Linq;
using CineTaquilla.Helpers;

namespace TaquillaITH.ViewModels
{
    public class DaySalesViewModel
    {
        //Sales table
        public int? UserId {get; set;}
        public TicketType TipoBoletoId { get; set; }
        public int? DaySalesId { get; set; }
        public DateTime SaleDate { get; set; }
        // Falta poner un campo de sala 
        //public string sala {get; set;}
        //Payments table
        public virtual Payment Payment { get; set; }
        public decimal Cash { get; set; }
        public decimal CreditCard { get; set; }
        public decimal RewardPoints { get; set; }
        public decimal TotalAmount { get; set; }
        //Movies table
        public string Name { get; set; }
        public string Schedule { get; set; }
        public string RunningTime { get; set; }
    }
}
