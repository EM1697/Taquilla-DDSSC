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
    public class PromotionsViewModel
    {
        List<Promotion> Promotions { get; set; }
    }
}
