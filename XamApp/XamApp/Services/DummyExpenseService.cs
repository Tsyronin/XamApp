using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Services
{
    public class DummyExpenseService
    {
        private List<Expense> Expenses = new List<Expense>();

        public DummyExpenseService()
        {
            Expenses.Add(new Expense()
            {
                Id = 0,
                UserBankAccountId = 2,
                CategoryId = 0,
                CategoryName = null,
                ExpenseIdentInBank = "ZMEp_ZKqngpMvGu5",
                Time = DateTime.Parse("2022-05-06T15:40:49"),
                Description = "Богодар / Bogodar",
                Amount = -158.96,
                Balance = 127315
            });

            Expenses.Add(new Expense()
            {
                Id = 0,
                UserBankAccountId = 2,
                CategoryId = 0,
                CategoryName = null,
                ExpenseIdentInBank = "1B9xRhCeW2ifPpgR",
                Time = DateTime.Parse("2022-05-03T12:42:57"),
                Description = "Нова пошта",
                Amount = -34753,
                Balance = 143211
            });
        }

        public IEnumerable<Expense> GetNotCheckedExpenses()
        {
            return Expenses;
        }

        public void AddExpense(Expense expense)
        {
            Expenses.Add(expense);
        }
    }

    public class Expense
    {
        public int Id { get; set; }
        public int UserBankAccountId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ExpenseIdentInBank { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Balance { get; set; }
    }
}
