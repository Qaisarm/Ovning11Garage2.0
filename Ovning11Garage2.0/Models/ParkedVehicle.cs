using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ovning11Garage2._0.Models
{
    public class ParkedVehicle
    {
        [Display(Name = "Number")]
        public int Id { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }
        [StringLength(6,MinimumLength =6)]
        [Required]
        public string RegistrationNumber { get; set; }
        public String Color { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        

        //Arival Time
        public DateTime TimeOfParking { get; set; }
    }

    public enum VehicleType
    {
        Car,
        Boat,
        Buss,
        Airplane,
        Cycle
    }
}
