using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripCalc.Models
{
    public class Trip
    {
        public Trip()
        {
            this.Expenses = new ObservableCollection<Expense>();
        }
        [Key]
        public int TripId { get; set; }

        public string TripName { get; set; }

        public double TripCost { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
