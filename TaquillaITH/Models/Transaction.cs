using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineTaquilla.Helpers;

namespace TaquillaITH.Models
{
    public class Transaction : Model
    {
        public string SenderFourDigits { get; set; }
        public string ReceiverFourDigits { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

