using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripCalc.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
