using System;
using TaquillaITH.Models;
using TaquillaITH.Data;
using System.Collections.Generic;
using System.Linq;
using CineTaquilla.Helpers;
using System.ComponentModel.DataAnnotations;

namespace TaquillaITH.ViewModels
{
    public class DaySalesViewModel
    {
        //Sales table
        public int? UserId {get; set;}
        public TicketType TipoBoletoId { get; set; }
        public int? DaySalesId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dddd dd, MMMM yyyy}")] 
        public DateTime SaleDate { get; set; }
        // Falta poner un campo de sala 
        //public string sala {get; set;}
        //Payments table
        public virtual Payment Payment { get; set; }
        [DisplayFormat(DataFormatString = "{0:\\$##0.00}")] 
        public decimal Cash { get; set; }
        [DisplayFormat(DataFormatString = "{0:\\$##0.00}")]
        public decimal CreditCard { get; set; }
        public decimal RewardPoints { get; set; }
        [DisplayFormat(DataFormatString = "{0:\\$##0.00}")]
        public decimal TotalAmount { get; set; }
        //Movies table
        public string Name { get; set; }
        public string Schedule { get; set; }
        public string RunningTime { get; set; }
        public string time { get; set; }
        public int saleId { get; set; }
    }
}
