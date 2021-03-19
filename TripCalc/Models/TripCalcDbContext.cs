using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripCalc.Models
{
    public class TripCalcDbContext : DbContext
    {
        public TripCalcDbContext(DbContextOptions<TripCalcDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
