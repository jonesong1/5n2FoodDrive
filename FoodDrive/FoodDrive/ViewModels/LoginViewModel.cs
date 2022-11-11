using FoodDrive.Models;
using FoodDrive.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoodDrive.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public User User { get; } = new User();
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var isValid = await App.UserManager.Login(User.Email, User.Password);
            if (isValid)
            {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "User is not valid", "Ok");
            }
        }
    }
}
