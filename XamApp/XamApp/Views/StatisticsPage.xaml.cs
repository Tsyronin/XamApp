using Microcharts;
using SkiaSharp;
using XamApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        StatisticsViewModel _viewModel;

        public StatisticsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new StatisticsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}