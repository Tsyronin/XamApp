using System;
using System.Collections.Generic;
using System.ComponentModel;
using XamApp.Models;
using XamApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Views
{
    public partial class NewExpensePage : ContentPage
    {
        public Item Item { get; set; }

        NewExpenseViewModel _viewModel;

        public NewExpensePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewExpenseViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}