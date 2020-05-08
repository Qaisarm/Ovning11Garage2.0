using System;
namespace Ovning11Garage2._0.Models.ViewModels
{
    public class ParkedVehicleViewModel
    {
        public int Id { get; set; }

        public VehicleType VehicleType { get; set; }

        public string RegistrationNumber { get; set; }
        public string Color { get; set; }

        //Arival Time
        public DateTime TimeOfParking { get; set; }

        public ParkedVehicleViewModel()
        {
        }
    }
}