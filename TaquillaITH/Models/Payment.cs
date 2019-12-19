using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CineTaquilla.Helpers;

namespace TaquillaITH.Models
{
    public class Payment : Model
    {
        [NotMapped]
        public bool usedCredit { get; set; }
        [NotMapped]
        public bool pointFlag { get; set; }

        public int Cash { get; set; }
        public int CreditCard { get; set; }
        public int RewardPoints { get; set; }
        public int TotalAmount { get => Cash + CreditCard + RewardPoints; }
    }
}

