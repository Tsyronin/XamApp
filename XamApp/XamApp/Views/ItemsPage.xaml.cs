using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamApp.ViewModels;
using Xamarin.Forms;

namespace XamApp.Views
{
    public partial class ItemsPage : ContentPage
    {
        NotCheckedExpensesViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NotCheckedExpensesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}