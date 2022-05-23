using System;
using System.Collections.Generic;
using System.Text;
using XamApp.Helpers;
using XamApp.Models;
using XamApp.Services;
using XamApp.Views;
using Xamarin.Forms;

namespace XamApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        public Command LoginCommand { get; }

        public Command SignUpTapped { get; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Message { get; set; }



        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            SignUpTapped = new Command(OnSignUpClicked);


            Email = Settings.Email;
            Password = Settings.Password;
        }

        private async void OnLoginClicked(object obj)
        {
            var loginModel = new AuthModel()
            {
                Email = Email,
                Password = Password
            };
            var accessToken = await _expenseService.LoginAsync(loginModel);
            if (accessToken != null)
            {
                Settings.AccessToken = accessToken;
                Settings.Email = Email;
                Settings.Password = Password;
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(MainMenuPage)}");
            }
            else
            {
                Message = "Invalid email or password";
            }
        }

        private async void OnSignUpClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
    }
}
