using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamApp.Models;
using XamApp.Services;
using Xamarin.Forms;

namespace XamApp.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        public IEnumerable<Statistic> Statistics { get; set; }
        public ChartEntry[] Entries { get; set; }
        public Chart Chart { get; private set; }
        public Command LoadStatisticsCommand { get; }

        public StatisticsViewModel()
        {
            Title = "Statistics";
            Statistics = new ObservableCollection<Statistic>();
            LoadStatisticsCommand = new Command(async () => await ExecuteLoadStatisticsCommand());
        }

        private async Task ExecuteLoadStatisticsCommand()
        {
            IsBusy = true;

            try
            {
                Statistics = await _expenseService.GetExpenseStatisticsAsync();
                Entries = GetStatisticChartEntries();
                Chart = new PieChart() { Entries = Entries, LabelTextSize = 50, LabelMode = LabelMode.RightOnly, AnimationDuration = TimeSpan.FromSeconds(0.7) };
                OnPropertyChanged(nameof(Chart));
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

        private ChartEntry[] GetStatisticChartEntries()
        {
            if (Statistics.Count() == 0)
                return new ChartEntry[0];
            //ExecuteLoadStatisticsCommand().Wait();

            Random rand = new Random();

            var statistic = Statistics.First();

            ChartEntry[] entries = statistic.Parts.Select(s => new ChartEntry((float)s.Amount)
            {
                Label = s.CategoryName,
                ValueLabel = s.Amount.ToString(),
                Color = SKColor.FromHsl(rand.Next(256), rand.Next(256), rand.Next(256))
            })
                .ToArray();

            return entries;
        }

        public void OnAppearing()
        {
            IsBusy = true;
            Entries = GetStatisticChartEntries();
        }
    }
}
