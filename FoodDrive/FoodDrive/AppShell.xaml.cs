using FoodDrive.ViewModels;
using FoodDrive.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FoodDrive
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));
            Routing.RegisterRoute(nameof(EventDetailPageNoToolbar), typeof(EventDetailPageNoToolbar));
            Routing.RegisterRoute(nameof(EventMapPage), typeof(EventMapPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await App.UserManager.Logout();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
