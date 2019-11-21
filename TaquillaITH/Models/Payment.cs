using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTaquilla.Helpers;

namespace TaquillaITH.Models
{
    public class Payment : Model
    {
        public decimal Cash { get; set; }
        public decimal CreditCard { get; set; }
        public decimal RewardPoints { get; set; }
        public decimal TotalAmount { get => Cash + CreditCard + RewardPoints; }
    }
}

