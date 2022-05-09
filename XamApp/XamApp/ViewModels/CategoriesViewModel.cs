using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using XamApp.Models;
using XamApp.Services;
using Xamarin.Forms;

namespace XamApp.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        private Category _selectedCategory;

        public ObservableCollection<Category> Categories { get; }

        public Command LoadCategoriesCommand { get; }

        public Command AddCategoryCommand { get; }

        public Command<Category> CategoryTapped { get; }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                OnCategorySelected(value);
            }
        }


        public CategoriesViewModel()
        {
            Title = "Categories";
            Categories = new ObservableCollection<Category>();
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());

            CategoryTapped = new Command<Category>(OnCategorySelected);

            AddCategoryCommand = new Command(OnAddCategory);
        }

        async Task ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;

            try
            {
                Categories.Clear();
                var expenses = await _expenseService.GetCategoriesAsync();

                foreach (var expense in expenses)
                {
                    Categories.Add(expense);
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
            SelectedCategory = null;
        }

        private async void OnAddCategory(object obj)
        {
            var response = await Application.Current.MainPage.DisplayPromptAsync("New category name", "", "OK", "Cancel");

            if (response == "Cancel" || String.IsNullOrEmpty(response))
                return;

            var newCategory = new Category() { Name = response };
            await _expenseService.AddCategoryAsync(newCategory);

            Categories.Add(newCategory);
        }

        async void OnCategorySelected(Category category)
        {
            if (category == null)
                return;

            var response = await Application.Current.MainPage.DisplayAlert("Delete category?", "", "OK", "Cancel");

            if (response == false)
                return;

            await _expenseService.DeleteCategoryAsync(category.Id);

            Categories.Remove(category);

            //if (response == "Cancel" || String.IsNullOrEmpty(response))
            //    return;


            // This will push the ItemDetailPage onto the navigation stack

            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ExpenseId)}={expense.Id}");
        }
    }
}
