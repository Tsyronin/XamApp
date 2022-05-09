using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XamApp.Helpers.SharedModels;
using XamApp.Models;
using XamApp.Services;
using XamApp.Views;
using Xamarin.Forms;

namespace XamApp.ViewModels
{
    public class BankExpensesViewModel : BaseViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        private Expense _selectedExpense;

        public ObservableCollection<Expense> Expenses { get; }
        public Command LoadExpensesCommand { get; }
        public Command AddExpenseCommand { get; }
        public Command<Expense> ExpenseTapped { get; }

        private IEnumerable<Category> Categories;

        public BankExpensesViewModel()
        {
            Title = "Last Bank Expenses";
            Expenses = new ObservableCollection<Expense>();
            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());

            ExpenseTapped = new Command<Expense>(OnExpenseSelected);

            AddExpenseCommand = new Command(OnAddExpense);
        }

        async Task ExecuteLoadExpensesCommand()
        {
            IsBusy = true;

            try
            {
                Expenses.Clear();
                var expenses = await _expenseService.GetNotCheckedExpensesAsync();
                Categories = await _expenseService.GetCategoriesAsync();

                foreach (var expense in expenses)
                {
                    Expenses.Add(expense);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedExpense = null;
        }

        public Expense SelectedExpense
        {
            get => _selectedExpense;
            set
            {
                SetProperty(ref _selectedExpense, value);
                OnExpenseSelected(value);
            }
        }

        private async void OnAddExpense(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewExpensePage));
        }

        async void OnExpenseSelected(Expense expense)
        {
            if (expense == null)
                return;

            SharedExpense.SelectedExpense = expense;

            var response = await Application.Current.MainPage.DisplayActionSheet("Category:", "Cancel", null, Categories.Select(c => c.Name).ToArray());
            if (response == "Cancel" || String.IsNullOrEmpty(response))
                return;
            Category selectedCategory = Categories.Single(c => c.Name == response);
            expense.CategoryId = selectedCategory.Id;
            expense.CategoryName = selectedCategory.Name;

            await _expenseService.AddExpenseAsync(expense);
            Expenses.Remove(expense);
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ExpenseId)}={expense.Id}");
        }
    }
}