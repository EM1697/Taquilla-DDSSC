using TaquillaITH.Data;
using TaquillaITH.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Services
{
    public class ApiServices : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApiServices(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
