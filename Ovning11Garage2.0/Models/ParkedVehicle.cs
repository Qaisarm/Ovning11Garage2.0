using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ovning11Garage2._0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }
       
        public VehicleType VehicleType { get; set; }
     
        public string RegistrationNumber { get; set; }
        public int NumberOfWheels { get; set; }
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
        MoterCycle,
        Buss,
        Airplane,
        Cycle
    }
}
