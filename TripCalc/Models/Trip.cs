using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripCalc.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        public string TripName { get; set; }

        public double TripCost { get; set; }
    }
}
