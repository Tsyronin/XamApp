using XamApp.ViewModels;
using Xamarin.Forms;

namespace XamApp.Views
{
    public partial class ExpenseHistoryPage : ContentPage
    {
        ExpenseHistoryViewModel _viewModel;

        public ExpenseHistoryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ExpenseHistoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}