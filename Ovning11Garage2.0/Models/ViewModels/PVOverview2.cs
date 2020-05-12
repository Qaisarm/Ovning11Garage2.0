using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ovning11Garage2._0.Models.ViewModels
{
    public class PVOverview2
    {

        [Display(Name = "Number")]
        public int Id { get; set; }

      
        [StringLength(6, MinimumLength = 6)]
        [Required]
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Range(2, 8)]
        public int NumberOfWheels { get; set; }
        
    }
}
