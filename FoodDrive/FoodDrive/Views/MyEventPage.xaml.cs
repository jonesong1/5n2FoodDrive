using FoodDrive.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyEventPage : ContentPage
    {
        MyEventViewModel _viewModel;
        public MyEventPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MyEventViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}