using FoodDrive.Models;
using FoodDrive.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FoodDrive.ViewModels
{
    [QueryProperty(nameof(EventId), nameof(EventId))]
    public class EventDetailViewModel : BaseViewModel
    {
        public IEventService<Event> DataStore => DependencyService.Get<IEventService<Event>>();
        private string eventId;
        private string name;
        private string description;
        private string address;
        private Geocoder _geoCoder;
        private Xamarin.Forms.Maps.Map myMap;
        private Position position;
        public Command SaveEventCommand { get; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Address
        {
            get => address;
            set=> SetProperty(ref address, value);
        }
        public string EventId
        {
            get
            {
                return eventId;
            }
            set
            {
                eventId = value;
                LoadEventId(value);
            }
        }
        public EventDetailViewModel(Xamarin.Forms.Maps.Map myMap)
        {
            this.myMap = myMap;
            _geoCoder = new Geocoder();
            SaveEventCommand = new Command(OnSaveEvent);
        }
        private async void OnSaveEvent(object obj)
        {
            var saveEvent = await DataStore.PostEventAsync(EventId);
            if (saveEvent)
            {
                await App.Current.MainPage.DisplayAlert("Alert", Name + "is saved in your favorite", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Error, please try again", "OK");
            }
        }
        public async void LoadEventId(string eventId)
        {
            try
            {
                var item = await DataStore.GetEventAsync(eventId);
                Name = item.Title;
                Description = item.Description;
                Address = item.FromStreet + ", " + item.FromCity;
                IEnumerable<Position> approximateLocations = await _geoCoder.GetPositionsForAddressAsync(Address);
                //var approximateLocations = await _geoCoder.GetPositionsForAddressAsync(Address);
                position = approximateLocations.FirstOrDefault();
                var Address2 = item.ToStreet + ", " + item.ToCity;
                IEnumerable< Position > approximateLocations2 = await _geoCoder.GetPositionsForAddressAsync(Address2);
                var position2 = approximateLocations2.FirstOrDefault();
                //LoadMap();
                Pin pinFromLoc = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Pick up location",
                    Address = Address,
                    Position = position,
                };
                myMap.Pins.Add(pinFromLoc);
                Pin pinToLoc = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Drop off location",
                    Address = Address2,
                    Position = position2,
                };
                myMap.Pins.Add(pinToLoc);
                Polyline polyline = new Polyline
                {
                    StrokeColor = Color.Blue,
                    StrokeWidth = 12

                };
                // add the polyline to the map's MapElements collection
                polyline.Geopath.Add(pinFromLoc.Position);
                polyline.Geopath.Add(pinToLoc.Position);
                myMap.MapElements.Add(polyline);
                myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinFromLoc.Position, Distance.FromMeters(4000)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        public void LoadMap()
        {
            Pin pinFromLoc = new Pin()
            {
                Type = PinType.Place,
                Label = Name,
                Address = Address,
                Position = position,
            };
            myMap.Pins.Add(pinFromLoc);
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinFromLoc.Position, Distance.FromMeters(1000)));
        }
    }
}
