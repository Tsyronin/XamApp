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

            //var entries = new[]
            //{
            //    new ChartEntry(212)
            //    {
            //        Label = "UWP",
            //        ValueLabel = "112",
            //        Color = SKColor.Parse("#2c3e50")
            //    },
            //    new ChartEntry(248)
            //    {
            //        Label = "Android",
            //        ValueLabel = "648",
            //        Color = SKColor.Parse("#77d065")
            //    },
            //    new ChartEntry(128)
            //    {
            //        Label = "iOS",
            //        ValueLabel = "428",
            //        Color = SKColor.Parse("#b455b6")
            //    },
            //    new ChartEntry(514)
            //    {
            //        Label = "Forms",
            //        ValueLabel = "214",
            //        Color = SKColor.Parse("#3498db")
            //    }
            //};

            //var entries = _viewModel.GetStatisticChartEntries();

            //chartViewPie.Chart = new PieChart() { Entries = _viewModel.Entries, LabelTextSize = 50, LabelMode = LabelMode.RightOnly };
        }
    }
}