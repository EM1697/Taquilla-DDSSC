using TaquillaITH.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext db;
        public DbInitializer(ApplicationDbContext _db)
        {
            db = _db;
        }
        //public async Task Seed(ApplicationDbContext db)
        //{
        //    if (!db.Departments.Any())
        //    {
        //        await CreateDepartment("Dulcería", 1);
        //        await CreateDepartment("Cafetería", 2);
        //        await db.SaveChangesAsync();
        //    }
        //}
        //public async Task CreateDepartment(string name, int depkey)
        //{
        //    Department model = new Department()
        //    {
        //        CreationDate = DateTime.UtcNow,
        //        LastUpdate = DateTime.UtcNow,
        //        DepartmentKey = depkey,
        //        Name = name
        //    };
        //    await db.Departments.AddAsync(model);
        //}
    }
}