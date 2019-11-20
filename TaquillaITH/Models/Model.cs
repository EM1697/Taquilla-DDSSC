using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaquillaITH.Models
{
    public abstract class Model
    {
        [ScaffoldColumn(false)]
        public Int32 Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime LastUpdate { get; set; }

        [ScaffoldColumn(false)]
        public Boolean IsDeleted { get; set; }
    }
}
