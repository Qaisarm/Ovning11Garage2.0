using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ovning11Garage2._0.Models;

namespace Ovning11Garage2._0.Data
{
    public class Ovning11Garage2_0Context : DbContext
    {
        public Ovning11Garage2_0Context (DbContextOptions<Ovning11Garage2_0Context> options)
            : base(options)
        {
        }

        public DbSet<Ovning11Garage2._0.Models.ParkedVehicle> ParkedVehicle { get; set; }
    }
}
