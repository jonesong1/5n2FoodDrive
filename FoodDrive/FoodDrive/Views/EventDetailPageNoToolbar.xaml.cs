using FoodDrive.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace FoodDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPageNoToolbar : ContentPage
    {
        public EventDetailPageNoToolbar()
        {
            InitializeComponent();
            BindingContext = new EventDetailViewModel(myMap);
        }
    }
}