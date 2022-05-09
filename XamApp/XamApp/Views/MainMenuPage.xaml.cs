using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : TabbedPage
    {
        MainMenuViewModel _viewModel;

        public MainMenuPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MainMenuViewModel();
        }
    }
}