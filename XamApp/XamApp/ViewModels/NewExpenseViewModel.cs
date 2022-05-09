using System;
using System.Collections.Generic;
using System.Linq;
using XamApp.Helpers.SharedModels;
using XamApp.Models;
using XamApp.Services;
using Xamarin.Forms;

namespace XamApp.ViewModels
{
    public class NewExpenseViewModel : BaseViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        public List<Category> Categories;

        public List<string> CategoryNames;


        private Category _selectedCategory;
        public Category SelectedCategory 
        {
            get { return _selectedCategory; }
            set 
            { 
                _selectedCategory = value;
                OnPropertyChanged(); 
            }
        }


        private string _selectedCategoryName;
        public string SelectedCategoryName
        {
            get { return _selectedCategoryName; }
            set
            {
                _selectedCategoryName = value;
                OnPropertyChanged();
            }
        }

        private DateTime expenseDate = DateTime.Now;
        public DateTime ExpenseDate
        {
            get { return expenseDate; }
            set
            {
                expenseDate = value;
                OnPropertyChanged(nameof(ExpenseDate));
            }
        }

        private double amount;

        public NewExpenseViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Categories = SharedCategories.Categories.ToList();
            //Categories = new List<Category>() { new Category() { Id = 0, Name = "cat Name" } };
            //CategoryNames = Categories.Select(c => c.Name).ToList();
        }

        private bool ValidateSave()
        {
            return Amount >= 0 
                && !String.IsNullOrEmpty(SelectedCategoryName);
        }

        public double Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var expense = new Expense()
            {
                Amount = -Amount,
                Time = ExpenseDate,
                CategoryId = Categories.Single(c => c.Name == SelectedCategoryName).Id,
                CategoryName = SelectedCategoryName
            };

            await _expenseService.AddExpenseAsync(expense);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public void OnAppearing()
        {
            //Categories = SharedCategories.Categories.ToList();
        }
    }
}
