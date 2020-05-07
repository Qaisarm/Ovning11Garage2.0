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
        [Required]
        [Range(2, 8)]
        public int NumberOfWheels { get; set; }
        public String Color { get; set; }
        [Required]
        public String Brand { get; set; }
        [Required]
        public String Model { get; set; }
        

        //Arival Time
        public DateTime TimeOfParking { get; set; }
    }

    public enum VehicleType
    {
        Car,
        Boat,
        MoterCycle,
        Buss,
        Airplane,
        Cycle
    }
}
