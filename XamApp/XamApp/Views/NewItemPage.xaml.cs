using System;
using System.Collections.Generic;
using System.ComponentModel;
using XamApp.Models;
using XamApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}