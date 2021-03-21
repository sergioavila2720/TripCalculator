using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using TripCalc.Controllers;
using TripCalc.Models;

namespace TripCalcTest
{

    [TestClass]
    public class CalculateTrip
    {

        // First test one person owes money 
        [TestMethod]
        public void TestMethod1()
        {
            List<string> resultActual = new List<string>();
            List<string> resultExpected = new List<string>();
            List<Expense> expense = new List<Expense>();
            List<Student> students = new List<Student>();            

            resultExpected.Add("Edwin owes $10 to Sergio");
            expense.Add(new Expense() {
                ExpenseId = 10,
                Name = "beach",
                StudentId = 10,
                TripId = 10,
                Price = 25.00F
            });
            expense.Add(new Expense()
            {
                ExpenseId = 11,
                Name = "food",
                StudentId = 11,
                TripId = 10,
                Price = 20.00F
            });
            expense.Add(new Expense()
            {
                ExpenseId = 12,
                Name = "Hotel",
                StudentId = 10,
                TripId = 10,
                Price = 15.00F
            });

            students.Add(new Student()
            {
                StudentId = 10,
                Name = "Sergio"
            });
            students.Add(new Student()
            {
                StudentId = 11,
                Name = "Edwin"
            });

            Trip trip = new Trip()
            {
                TripId = 10,
                TripCost = 60,
                TripName = "Iceland",
                Expenses = expense
            };
            foreach(var i in expense)
            {
                i.Trip = trip;
            }

            resultActual = ExpensesApiController.CalculateEvenlyFuncForTest(expense, students);

            CollectionAssert.AreEqual(resultExpected, resultActual, StructuralComparisons.StructuralComparer);

        }

        // multiple people owes one person
        [TestMethod]
        public void TestMethod2()
        {

            List<string> resultActual = new List<string>();
            List<string> resultExpected = new List<string>();
            List<Expense> expense = new List<Expense>();
            List<Student> students = new List<Student>();

            resultExpected.Add("Enrique owes $70 to Sergio");
            resultExpected.Add("Breana owes $40 to Sergio");
            expense.Add(new Expense()
            {
                ExpenseId = 10,
                Name = "beach",
                StudentId = 10,
                TripId = 10,
                Price = 200.00F
            });
            expense.Add(new Expense()
            {
                ExpenseId = 11,
                Name = "food",
                StudentId = 11,
                TripId = 10,
                Price = 20.00F
            });
            expense.Add(new Expense()
            {
                ExpenseId = 12,
                Name = "Hotel",
                StudentId = 12,
                TripId = 10,
                Price = 50.00F
            });

            students.Add(new Student()
            {
                StudentId = 10,
                Name = "Sergio"
            });
            students.Add(new Student()
            {
                StudentId = 11,
                Name = "Enrique"
            });
            students.Add(new Student()
            {
                StudentId = 12,
                Name = "Breana"
            });

            Trip trip = new Trip()
            {
                TripId = 10,
                TripCost = 270,
                TripName = "Beach",
                Expenses = expense
            };
            foreach (var i in expense)
            {
                i.Trip = trip;
            }

            resultActual = ExpensesApiController.CalculateEvenlyFuncForTest(expense, students);

            CollectionAssert.AreEqual(resultExpected, resultActual, StructuralComparisons.StructuralComparer, "What happened");

        }

        // no one owes any money
        [TestMethod]
        public void TestMethod3()
        {

            List<string> resultActual = new List<string>();
            List<string> resultExpected = new List<string>();
            List<Expense> expense = new List<Expense>();
            List<Student> students = new List<Student>();

            resultExpected.Add("No one owes money!");
            expense.Add(new Expense()
            {
                ExpenseId = 10,
                Name = "beach",
                StudentId = 10,
                TripId = 10,
                Price = 200.00F
            });
            expense.Add(new Expense()
            {
                ExpenseId = 11,
                Name = "food",
                StudentId = 11,
                TripId = 10,
                Price = 200.00F
            });
            expense.Add(new Expense()
            {
                ExpenseId = 12,
                Name = "Hotel",
                StudentId = 12,
                TripId = 10,
                Price = 200.00F
            });

            students.Add(new Student()
            {
                StudentId = 10,
                Name = "Sergio"
            });
            students.Add(new Student()
            {
                StudentId = 11,
                Name = "Enrique"
            });
            students.Add(new Student()
            {
                StudentId = 12,
                Name = "Breana"
            });

            Trip trip = new Trip()
            {
                TripId = 10,
                TripCost = 600,
                TripName = "Beach",
                Expenses = expense
            };
            foreach (var i in expense)
            {
                i.Trip = trip;
            }

            resultActual = ExpensesApiController.CalculateEvenlyFuncForTest(expense, students);

            CollectionAssert.AreEqual(resultExpected, resultActual, StructuralComparisons.StructuralComparer);

        }
    }
}
