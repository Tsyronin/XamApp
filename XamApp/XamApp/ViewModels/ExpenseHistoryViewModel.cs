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
    public class ExpenseHistoryViewModel : BaseViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        private Expense _selectedExpense;

        public ObservableCollection<Expense> Expenses { get; }
        public Command LoadExpensesCommand { get; }
        public Command AddExpenseCommand { get; }
        public Command<Expense> ExpenseTapped { get; }

        public ExpenseHistoryViewModel()
        {
            Title = "Expense History";
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
                var expenses = await _expenseService.GetExpenseHistoryAsync();

                foreach (var expense in expenses.OrderByDescending(e => e.Time))
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

            var response = await Application.Current.MainPage.DisplayAlert("Delete expense?", "", "OK", "Cancel");

            if (response == false)
                return;

            await _expenseService.DeleteExpense(expense.Id);

            Expenses.Remove(expense);

            //if (response == "Cancel" || String.IsNullOrEmpty(response))
            //    return;


            // This will push the ItemDetailPage onto the navigation stack

            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ExpenseId)}={expense.Id}");
        }
    }
}
