using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripCalc.HelperClasses;
using TripCalc.Models;

namespace TripCalc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesApiController : ControllerBase
    {
        private readonly TripCalcDbContext _context;

        public ExpensesApiController(TripCalcDbContext context)
        {
            _context = context;
        }

        // GET: api/ExpensesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/ExpensesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);


            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        [HttpGet("tripid={id}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesByTripId(int id)
        {
            return await _context.Expenses.Where(t0 => t0.Trip.TripId == id)
                                          .Include("Trip")
                                          .Include("Student").ToListAsync();
        }

        [HttpGet("calculate/trip={id}")]
        public ActionResult<IEnumerable<string>> CalculateTripEvenly(int id)
        {
            return HelperFunc(id).ToList();
        }

        // PUT: api/ExpensesApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, [FromForm] Expense expense)
        {
            expense.ExpenseId = id;

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ExpensesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{id}")]
        public async Task<ActionResult<Expense>> PostExpense(int id, [FromForm]Expense expense)
        {
            if (id >0)
                expense.TripId = id;
            try 
            {
                _context.Expenses.Add(expense);

                // Calculate the total trip cost based on the expenses
                Trip trip = _context.Trips.Include("Expenses").Where(t0 => t0.TripId == id).FirstOrDefault();
                double tripsum = 0.0;

                foreach (var item in trip.Expenses)
                {
                    tripsum += item.Price;
                }

                trip.TripCost = tripsum;

                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
            

            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/ExpensesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return expense;
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }

        private List<string> HelperFunc(int id)
        {
            //Get expenses based on trip id
            List<Expense> expenses = _context.Expenses.Where(t0 => t0.Trip.TripId == id)
                                          .Include("Trip")
                                          .Include("Student").ToList();

            // total cost by count of Stundents
            double totalTripCost = expenses.FirstOrDefault().Trip.TripCost;
            double average = totalTripCost / expenses.GroupBy(t0 => t0.StudentId).Count();                      
            
            // get a list containing the student's name sum of expenses and order them by smaller to larger to be able to tell debtors and creditors appart 
            var query = (from ex in expenses
                                 join st in _context.Students
                                 on ex.StudentId equals st.StudentId
                                 group ex by st.Name into g
                                 select new { name = g.Key, sum = g.Sum(t0 => t0.Price), paidAvgDiff =  (g.Sum(t0 => t0.Price)) - average }).OrderBy(t2 =>t2.paidAvgDiff).ToList();

            // cast the query to a helper function because var are read-only
            List<StudentExpenses> StudentTotals = new List<StudentExpenses>();
            foreach (var student in query)
            {
                StudentTotals.Add(new StudentExpenses()
                {
                    name = student.name,
                    sum = student.sum,
                    paidAvgDiff = student.paidAvgDiff
                });  

            }

            //Begin algorithm
            // need to keep track of indexes of people who own and people who are owened 
            int i = 0; // owes money
            int j = StudentTotals.Count() - 1; // is owed
            double debt = 0.0;
            List<string> output = new List<string>();
            while (i < j)
            {
                debt = Math.Min(-(StudentTotals[i].paidAvgDiff), StudentTotals[j].paidAvgDiff);
                StudentTotals[i].paidAvgDiff += debt; // owes money
                StudentTotals[j].paidAvgDiff -= debt; // is owed money 
                output.Add(StudentTotals[i].name + " owes " + "$" +debt + " to " + StudentTotals[j].name );
                // increment and decrement debits and credits
                if (StudentTotals[i].paidAvgDiff == 0)
                    i++;
                
                if (StudentTotals[j].paidAvgDiff == 0)
                    j--;
            };
            return output;
        }     

    }
}
