using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ovning11Garage2._0.Models.ViewModels
{
    public class ReceiptViewModel
    {

        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }
        [Display(Name = "Registration Number: ")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "CheckIn Time: ")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime TimeOfParking { get; set; }

        [Display(Name = "CheckOut Time: ")]
        public DateTime TimeOfCheckOut { get; set; }

        [Display(Name = "Total Parking Time: ")]
        public string TotalTime { get; set; }

        [Display(Name = "Total Price: ")]
        public string TotalPrice { get; set; }
    }
}
