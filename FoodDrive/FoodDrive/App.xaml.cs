using FoodDrive.Services;
using FoodDrive.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDrive
{
    public partial class App : Application
    {
        public static UserManager UserManager { get; private set; }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<EventService>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginPage());
            UserManager = new UserManager(new UserService());
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
