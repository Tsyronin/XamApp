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
    public partial class BankExpensesPage : ContentPage
    {
        BankExpensesViewModel _viewModel;

        public BankExpensesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new BankExpensesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}