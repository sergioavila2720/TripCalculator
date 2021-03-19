using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

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
    }
}
