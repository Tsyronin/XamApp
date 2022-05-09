using System;
using System.Diagnostics;
using System.Threading.Tasks;
using XamApp.Helpers.SharedModels;
using XamApp.Models;
using Xamarin.Forms;

namespace XamApp.ViewModels
{
    [QueryProperty(nameof(ExpenseId), nameof(ExpenseId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string expenseId;
        private double amount;
        private string description;
        public string Id { get; set; }

        private Expense expense;

        public double Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ExpenseId
        {
            get
            {
                return expenseId;
            }
            set
            {
                expenseId = value;
                LoadExpenseId(value);
            }
        }

        public void LoadExpenseId(string expenseId)
        {
            try
            {
                //var item = await DataStore.GetItemAsync(itemId);
                expense = SharedExpense.SelectedExpense;
                Id = expense.Id.ToString();
                Amount = expense.Amount;
                Description = expense.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
