using System;
using TaquillaITH.Models;
using TaquillaITH.Data;
using System.Collections.Generic;
using System.Linq;
using CineTaquilla.Helpers;
using System.ComponentModel.DataAnnotations;

namespace TaquillaITH.ViewModels
{
    public class PromotionsViewModel
    {
        public string ProductId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class PromotionsListViewModel
    {
        public List<PromotionsViewModel> Promotions { get; set; }
    }
}
