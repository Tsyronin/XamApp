using System;
using XamApp.Helpers;
using XamApp.Services;
using XamApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<ExpenseService>();

            SetMainPage();
            //MainPage = new AppShell();
        }

        private void SetMainPage()
        {
            if (!String.IsNullOrEmpty(Settings.AccessToken))
            {
                MainPage = new AppShell();
            }
            else if (!String.IsNullOrEmpty(Settings.Email)
                && !String.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
