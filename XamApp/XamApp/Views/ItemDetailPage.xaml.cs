using System.ComponentModel;
using XamApp.ViewModels;
using Xamarin.Forms;

namespace XamApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}