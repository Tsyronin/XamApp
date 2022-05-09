using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamApp.Models;
using XamApp.Services;
using Xamarin.Forms;

namespace XamApp.ViewModels
{
    public class AddExpenseViewModel : BaseViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        public List<Category> Categories;

        public List<string> CategoryNames;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }


        public AddExpenseViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            Categories = new List<Category>() { new Category() { Id = 0, Name = "cat Name" } };
            CategoryNames = Categories.Select(c => c.Name).ToList();

        }

        private bool ValidateSave()
        {
            return true;
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
