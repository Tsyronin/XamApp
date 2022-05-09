using System;
using System.Collections.Generic;
using System.Text;
using XamApp.Helpers.SharedModels;
using XamApp.Services;
using XamApp.Views;
using Xamarin.Forms;

namespace XamApp.ViewModels
{
    public class MainMenuViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        public Command AddExpenseCommand { get; }
        public Command ViewCategoriesCommand { get; }

        public MainMenuViewModel()
        {
            AddExpenseCommand = new Command(OnAddExpense);
            ViewCategoriesCommand = new Command(OnViewCategories);
        }

        private async void OnAddExpense(object obj)
        {
            SharedCategories.Categories = await _expenseService.GetCategoriesAsync();
            await Shell.Current.GoToAsync(nameof(NewExpensePage));
        }

        private async void OnViewCategories(object obj)
        {
            SharedCategories.Categories = await _expenseService.GetCategoriesAsync();
            await Shell.Current.GoToAsync(nameof(CategoriesPage));
        }
    }
}
