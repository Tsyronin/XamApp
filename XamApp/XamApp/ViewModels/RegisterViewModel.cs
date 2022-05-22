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
    public class RegisterViewModel : BaseViewModel
    {
        public ExpenseService _expenseService => DependencyService.Get<ExpenseService>();

        public Command RegisterCommand { get; }

        public Command SignInTapped { get; }


        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Message { get; set; }



        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
            SignInTapped = new Command(OnSignInClicked);

            Email = Settings.Email;
            Password = Settings.Password;
        }

        private async void OnRegisterClicked(object obj)
        {
            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match";
                return;
            }
            var registerModel = new AuthModel()
            {
                Email = Email,
                Password = Password
            };
            bool success = await _expenseService.RegisterAsync(registerModel);
            if (success)
            {
                Settings.Email = Email;
                Settings.Password = Password;
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                Message = "Error registering";
            }
        }

        private async void OnSignInClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

    }
}
